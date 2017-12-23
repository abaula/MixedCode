using System.Diagnostics;

namespace RabbitMqExperiments.LauncherApp
{
    class Program
    {
        static void Main()
        {
            using (var process1 = new Process())
            {
                process1.StartInfo.UseShellExecute = true;
                process1.StartInfo.FileName = "dotnet.exe";
                process1.StartInfo.Arguments = @"..\RabbitMqExperiments.ConsumerApp\bin\Debug\netcoreapp2.0\RabbitMqExperiments.ConsumerApp.dll";
                process1.Start();
            }

            using (var process2 = new Process())
            {
                process2.StartInfo.UseShellExecute = true;
                process2.StartInfo.FileName = "dotnet.exe";
                process2.StartInfo.Arguments = @"..\RabbitMqExperiments.ProviderApp\bin\Debug\netcoreapp2.0\RabbitMqExperiments.ProviderApp.dll";
                process2.Start();
            }
        }
    }
}
