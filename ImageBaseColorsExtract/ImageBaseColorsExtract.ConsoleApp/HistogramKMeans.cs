using ImageMagick;
using System;
using System.Collections.Generic;

namespace ImageBaseColorsExtract.ConsoleApp
{
    struct DataPoint<T>
        where T : struct
    {
        public T R;
        public T G;
        public T B;
        public T A;
    }

    struct ImgPoint
    {
        public DataPoint<byte> Color;
        public uint Weight;
        public int Cluster;
    }

    class HistogramKMeans
    {
        private readonly IReadOnlyDictionary<IMagickColor<byte>, uint> _histogram;

        public HistogramKMeans(IReadOnlyDictionary<IMagickColor<byte>, uint> histogram)
        {
            _histogram = histogram;
        }

        public (MagickColor, double)[] Cluster(int numberOfClusters, int maxTryCount)
        {
            int count = _histogram.Count;
            // Картинки могут быть большими поэтому выделяем память в куче.
            Span<ImgPoint> imgPoints = new ImgPoint[count].AsSpan();
            // Инициализируем данные всех точек картинки.
            InitializeImgPoints(imgPoints);
            // Инициализируем данные кластеров для каждой точки картинки.
            InitClustering(imgPoints, numberOfClusters);

            // Создаём данные means для каждого кластера.
            Span<DataPoint<double>> means = stackalloc DataPoint<double>[numberOfClusters];

            var changed = true;
            var success = true;
            var counter = 0;

            while (changed && success && counter < maxTryCount)
            {
                ++counter;
                success = UpdateMeans(imgPoints, means);
                changed = UpdateClustering(imgPoints, means);
            }

            var centers = new List<(MagickColor, double)>();

            for (var i = 0; i < numberOfClusters; i++)
            {
                var clusterItemCount = CountClusterItems(imgPoints, i);
                var r = Convert.ToByte(means[i].R);
                var g = Convert.ToByte(means[i].G);
                var b = Convert.ToByte(means[i].B);
                var a = Convert.ToByte(means[i].A);
                centers.Add((new MagickColor(r, g, b, a), (double)clusterItemCount / imgPoints.Length));
            }

            return centers.ToArray();
        }

        private static bool UpdateMeans(Span<ImgPoint> data, Span<DataPoint<double>> means)
        {
            var numClusters = means.Length;
            Span<uint> clusterItemsCount = stackalloc uint[numClusters];

            for (var i = 0; i < data.Length; i++)
            {
                var cluster = data[i].Cluster;
                clusterItemsCount[cluster] += data[i].Weight;
            }

            // Если хотя бы один из кластеров не содержит данных, возвращаем false.
            for (var k = 0; k < numClusters; k++)
            {
                if (clusterItemsCount[k] == 0)
                    return false;
            }

            // Zero means.
            for (var k = 0; k < means.Length; k++)
            {
                means[k].R = 0.0;
                means[k].G = 0.0;
                means[k].B = 0.0;
                means[k].A = 0.0;
            }

            // accumulate sum
            for (var i = 0; i < data.Length; i++)
            {
                var point = data[i];
                var cluster = point.Cluster;
                var weight = point.Weight;
                var color = point.Color;
                means[cluster].R += color.R * weight;
                means[cluster].G += color.G * weight;
                means[cluster].B += color.B * weight;
                means[cluster].A += color.A * weight;
            }

            // calculate mean value
            for (var k = 0; k < means.Length; k++)
            {
                var clusterCount = clusterItemsCount[k];
                means[k].R /= clusterCount;
                means[k].G /= clusterCount;
                means[k].B /= clusterCount;
                means[k].A /= clusterCount;
            }

            return true;
        }

        private static bool UpdateClustering(Span<ImgPoint> data, Span<DataPoint<double>> means)
        {
            var numClusters = means.Length;
            var changed = false;
            Span<int> newClustering = new int[data.Length].AsSpan();

            // Копируем текущие значения кластера для каждой точки картинки.
            for (var i = 0; i < data.Length; i++)
                newClustering[i] = data[i].Cluster;

            Span<double> distances = stackalloc double[numClusters];

            for (var i = 0; i < data.Length; i++)
            {
                for (var k = 0; k < numClusters; k++)
                    distances[k] = Distance(data[i].Color, means[k]);

                var newClusterId = MinIndex(distances);

                if (newClusterId != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterId;
                }
            }

            // Если не было изменений, возвращаем false.
            if (changed == false)
                return false;

            // Если хотя бы один из кластеров не содержит данных, возвращаем false.
            for (var k = 0; k < numClusters; k++)
            {
                var clusterCount = CountClusterItems(newClustering, k);

                if (clusterCount == 0)
                    return false;
            }

            // Записываем новые значения кластеров для каждой точки картинки.
            for (var i = 0; i < data.Length; i++)
                data[i].Cluster = newClustering[i];

            return true; // no zero-counts and at least one change
        }

        private static double Distance(DataPoint<byte> point, DataPoint<double> mean)
        {
            var sumSquaredDiffs = 0.0;
            sumSquaredDiffs += Math.Pow(point.R - mean.R, 2);
            sumSquaredDiffs += Math.Pow(point.G - mean.G, 2);
            sumSquaredDiffs += Math.Pow(point.B - mean.B, 2);
            sumSquaredDiffs += Math.Pow(point.A - mean.A, 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(Span<double> distances)
        {
            var indexOfMin = 0;
            var smallDist = distances[0];

            for (var k = 0; k < distances.Length; k++)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }

            return indexOfMin;
        }

        private static void InitClustering(Span<ImgPoint> imgPoints, int numClusters, int? seed = null)
        {
            var random = seed == null ? new Random() : new Random(seed.Value);

            // для правильной работы, обязательно должны присутствовать индексы всех кластеров.
            for (var i = 0; i < numClusters; i++)
                imgPoints[i].Cluster = i;

            // Далее заполняем случайными значениями.
            for (var i = numClusters; i < imgPoints.Length; i++)
                imgPoints[i].Cluster = random.Next(0, numClusters);
        }

        private void InitializeImgPoints(Span<ImgPoint> data)
        {
            var i = 0;

            foreach (var pair in _histogram)
            {
                var color = pair.Key;
                data[i].Color.R = color.R;
                data[i].Color.G =  color.G;
                data[i].Color.B = color.B;
                data[i].Color.A = color.A;
                data[i].Weight = pair.Value;
                i++;
            }
        }

        private static int CountClusterItems(Span<ImgPoint> data, int cluster)
        {
            var count = 0;

            for (var i = 0; i < data.Length; i++)
            {
                if (data[i].Cluster == cluster)
                    count++;
            }

            return count;
        }

        private static int CountClusterItems(Span<int> clusters, int cluster)
        {
            var count = 0;

            for (var i = 0; i < clusters.Length; i++)
            {
                if (clusters[i] == cluster)
                    count++;
            }

            return count;
        }
    }
}
