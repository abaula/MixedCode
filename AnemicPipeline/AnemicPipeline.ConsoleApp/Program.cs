using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace AnemicPipeline.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddWorkflow()
                .BuildServiceProvider();

            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HelloWorldWorkflow>();
            host.Start();

            Task.WaitAll(
                host.StartWorkflow(nameof(HelloWorldWorkflow), 1, null),
                host.StartWorkflow(nameof(HelloWorldWorkflow), 1, null)
            );

            Console.ReadKey();
            host.Stop();
        }
    }
}
