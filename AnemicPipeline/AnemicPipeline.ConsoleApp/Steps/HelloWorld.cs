using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace AnemicPipeline.ConsoleApp.Steps
{
    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }
}