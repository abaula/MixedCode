using System.IO;

namespace ImageBaseColorsExtract.ConsoleApp
{
    class Program
    {
        private const int NumberOfBaseColors = 4;

        static void Main()
        {
            var basePath = @"C:\ImageBaseColorsExtract";
            ListBaseColorsFromImagesInPath($@"{basePath}\input_images", $@"{basePath}\output_images");
        }

        private static void ListBaseColorsFromImagesInPath(string inputPath, string outputPath)
        {
            var inputDir = new DirectoryInfo(inputPath);
            var extracter = new ImageDominantColorsExtracter();

            foreach (var inputFile in inputDir.GetFiles())
            {
                var outputFile = $"{outputPath}\\{inputFile.Name}.jpg";
                extracter.MakeImageWithBaseColors(inputFile.FullName, outputFile, NumberOfBaseColors);
            }
        }
    }
}
