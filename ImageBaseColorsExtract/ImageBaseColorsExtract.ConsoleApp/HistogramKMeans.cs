using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageBaseColorsExtract.ConsoleApp
{
    /// <summary>
    /// based on https://visualstudiomagazine.com/articles/2013/12/01/k-means-data-clustering-using-c.aspx 
    /// </summary>
    class HistogramKMeans
    {
        private readonly IReadOnlyDictionary<MagickColor, int> _histogram;

        public HistogramKMeans(IReadOnlyDictionary<MagickColor, int> histogram)
        {
            _histogram = histogram;
        }

        public (MagickColor, double)[] Cluster(int numberOfClusters, int maxTryCount)
        {
            var (data, weights) = Initialize();
            var clustering = InitClustering(data.Length, numberOfClusters);
            var means = AllocateMeans(numberOfClusters, data[0].Length);

            var changed = true;
            var success = true;
            var counter = 0;

            while (changed && success && counter < maxTryCount)
            {
                ++counter;
                success = UpdateMeans(data, weights, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }

            var centers = new List<(MagickColor, double)>();

            for (var i = 0; i < numberOfClusters; i++)
            {
                var clusterItemCount = clustering.Count(_ => _ == i);
                var r = Convert.ToByte(means[i][0]);
                var g = Convert.ToByte(means[i][1]);
                var b = Convert.ToByte(means[i][2]);
                var a = Convert.ToByte(means[i][3]);
                centers.Add((new MagickColor(r, g, b, a), (double)clusterItemCount / data.Length));
            }

            return centers.ToArray();
        }

        private static bool UpdateMeans(double[][] data, int[] weights, int[] clustering, double[][] means)
        {
            var numClusters = means.Length;
            var clusterCounts = new int[numClusters];

            for (var i = 0; i < data.Length; i++)
            {
                var cluster = clustering[i];
                clusterCounts[cluster] += weights[i];
            }

            for (var k = 0; k < numClusters; k++)
            {
                if (clusterCounts[k] == 0)
                {
                    return false;
                }
            }

            for (var k = 0; k < means.Length; k++)
            {
                for (var j = 0; j < means[k].Length; j++)
                {
                    means[k][j] = 0.0;
                }
            }

            for (var i = 0; i < data.Length; i++)
            {
                var cluster = clustering[i];
                for (var j = 0; j < data[i].Length; j++)
                {
                    means[cluster][j] += data[i][j] * weights[i]; // accumulate sum
                }
            }

            for (var k = 0; k < means.Length; k++)
            {
                for (var j = 0; j < means[k].Length; j++)
                {
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
                }
            }

            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            var numClusters = means.Length;
            var changed = false;
            var newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);
            var distances = new double[numClusters];

            for (var i = 0; i < data.Length; i++)
            {
                for (var k = 0; k < numClusters; k++)
                {
                    distances[k] = Distance(data[i], means[k]);
                }

                var newClusterId = MinIndex(distances);

                if (newClusterId != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterId;
                }
            }

            if (changed == false)
            {
                return false;
            }

            var clusterCounts = new int[numClusters];

            for (var i = 0; i < data.Length; i++)
            {
                var cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (var k = 0; k < numClusters; k++)
            {
                if (clusterCounts[k] == 0)
                {
                    return false;
                }
            }

            Array.Copy(newClustering, clustering, newClustering.Length);
            return true; // no zero-counts and at least one change
        }

        private static double Distance(double[] tuple, double[] mean)
        {
            var sumSquaredDiffs = 0.0;

            for (var j = 0; j < tuple.Length; j++)
            {
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            }

            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
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

        private static double[][] AllocateMeans(int numClusters, int numDimentions)
        {
            var result = new double[numClusters][];

            for (var i = 0; i < numClusters; i++)
            {
                result[i] = new double[numDimentions];
            }

            return result;
        }

        private static int[] InitClustering(int dataLength, int numClusters, int? seed = null)
        {
            var random = seed == null ? new Random() : new Random(seed.Value);
            var clustering = new int[dataLength];

            for (var i = 0; i < numClusters; i++)
            {
                clustering[i] = i;
            }

            for (var i = numClusters; i < clustering.Length; i++)
            {
                clustering[i] = random.Next(0, numClusters);
            }

            return clustering;
        }

        private (double[][], int[]) Initialize()
        {
            var data = new double[_histogram.Count][];
            var weights = new int[_histogram.Count];
            var i = 0;

            foreach (var pair in _histogram)
            {
                var color = pair.Key;
                data[i] = new double[] { color.R, color.G, color.B, color.A };
                weights[i] = pair.Value;
                i++;
            }

            return (data, weights);
        }
    }
}
