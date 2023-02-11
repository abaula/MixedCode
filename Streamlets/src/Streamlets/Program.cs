using Streamlets.Samples;

namespace Streamlets
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine(typeof(Program).FullName);


            var factory = StreamletFactoryBuilder.CreateStreamlet<InputDto, OutputDto>("stml_01",
                stml =>
                {
                    stml.MapInputToVariable("var_input");
                    stml.MapOutputToVariable("var_output");

                    stml.Operations.Add<ISomeOperation, OutputDto>(op => op.Get(stml.Variable<InputDto>("var_input")))
                        .OnResult(res => stml.Variable("var_output", res));

                    stml.Operations.AddAsync<ISomeOperation, OutputDto>(async op => await op.GetAsync(stml.Variable<InputDto>("var_input")))
                        .OnResult(res => stml.Variable("var_output", res));

                })
                .Build();

            var stml = factory.GetStreamlet<InputDto, OutputDto>("stml_01");
            stml.Run(new InputDto());
            var result = stml.Result();

            return;
        }
    }
}