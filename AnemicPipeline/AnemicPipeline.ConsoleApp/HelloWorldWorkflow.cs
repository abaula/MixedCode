using AnemicPipeline.ConsoleApp.Steps;
using WorkflowCore.Interface;

namespace AnemicPipeline.ConsoleApp
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => nameof(HelloWorldWorkflow);
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }
    }
}