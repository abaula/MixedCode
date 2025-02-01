using System.Linq;
using ImageMagick;

namespace ImageBaseColorsExtract.ConsoleApp
{
    class ImageDominantColorsExtracter
    {
        public void MakeImageWithBaseColors(string inputFile, string outputFile, int numberOfColors)
        {
            var dominantColors = GetDominantColorsFromImage(inputFile, numberOfColors);
            MakeNewImage(inputFile, outputFile, dominantColors);
        }

        private void MakeNewImage(string inputFile, string outputFile, (MagickColor, double)[] dominantColors)
        {
            using (var originalImage = new MagickImage(inputFile))
            {
                ConvertToSRgb(originalImage);
                var width = (int)(originalImage.Width * 1.2);
                var height = originalImage.Height;

                using (var image = new MagickImage(new MagickColor("#ffffff"), width, height))
                {
                    ConvertToSRgb(image);
                    image.Composite(originalImage, 0, 0, CompositeOperator.Over);

                    var xOffset = originalImage.Width;
                    var yOffset = 0.0;

                    foreach (var (color, rate) in dominantColors.OrderByDescending(_ => _.Item2))
                    {
                        var lowerRightY = yOffset + (height * rate);

                        new Drawables()
                            .FillColor(color)
                            .Rectangle(xOffset, yOffset, width, lowerRightY)
                            .Draw(image);

                        yOffset = lowerRightY;
                    }

                    image.Write(outputFile);
                }
            }
        }

        private (MagickColor, double)[] GetDominantColorsFromImage(string file, int numberOfColors)
        {
            using (var image = new MagickImage(file))
            {
                ConvertToSRgb(image);
                var size = new MagickGeometry(300, 0);
                image.Resize(size);
                var histogram = image.Histogram();
                var histKMeans = new HistogramKMeans(histogram);
                var res = histKMeans.Cluster(numberOfColors, 100);

                return res;
            }
        }

        private static void ConvertToSRgb(MagickImage image)
        {
            if (image.ColorSpace == ColorSpace.sRGB)
            {
                return;
            }

            image.AddProfile(ColorProfile.USWebCoatedSWOP);
            image.AddProfile(ColorProfile.SRGB);
            image.ColorSpace = ColorSpace.sRGB;
        }
    }
}
