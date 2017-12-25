using System.Diagnostics;

namespace RabbitMqExperiments.LauncherApp
{
    class Program
    {
        static void Main()
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "dotnet.exe";
                process.StartInfo.Arguments = @"..\RabbitMqExperiments.ConsumerApp\bin\Debug\netcoreapp2.0\RabbitMqExperiments.ConsumerApp.dll";
                process.Start();
            }

            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "dotnet.exe";
                process.StartInfo.Arguments = @"..\RabbitMqExperiments.ProviderApp\bin\Debug\netcoreapp2.0\RabbitMqExperiments.ProviderApp.dll";
                process.Start();
            }

            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "dotnet.exe";
                process.StartInfo.Arguments = @"..\RabbitMqExperiments.LoggerApp\bin\Debug\netcoreapp2.0\RabbitMqExperiments.LoggerApp.dll";
                process.Start();
            }
        }
    }
}
