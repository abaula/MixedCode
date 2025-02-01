Рабочее название - `streamlets` (ручейки)

### 1. Простой `streamlet`.


```
var factory = StreamletFactoryBuilder
    .CreateStreamlet<InputDto, OutputDto>(
        "stml_01",
        stml =>
        {
            stml.MapInputToVariable("var_input");
            stml.MapOutputToVariable("var_output");

            stml.Operations.Add<IAdditionalDataProvider>(opt => opt.Operation.Get(stml.Variable("var_input")))
                .Result<AdditionalDataDto>(res => stml.Variable("var_additional_data", res));

            stml.Operations.Add<ITransformation>(opt => opt.Operation.Transform(stml.Variable("var_input"), stml.Variable("var_additional_data")))
                .Result<OutputDto>(res => stml.Variable("var_output", res));

            stml.CatchExceptions.Add<InvalidOperationException>(opt => stml.Variable("var_output", new OutputDto { Error = opt.Exception }));
        }
    )
    .Build();
```

### 2. Вызов одного `streamlet` из другого.


```
var factory = StreamletFactoryBuilder
    .CreateStreamlet<InputDto, StmlOutputDto>(
        "stml_01",
        stml =>
        {
            stml.MapInputToVariable("var_input");
            stml.MapOutputToVariable("var_output");

            stml.Operations.Add<IAdditionalDataProvider>(opt => opt.Operation.Get(stml.Variable("var_input")))
                .Result<AdditionalDataDto>(res => stml.Variable("var_additional_data", res));

            stml.Operations.Add<ITransformation>(opt => opt.Operation.Transform(stml.Variable("var_input"), stml.Variable("var_additional_data")))
                .Result<OutputDto>(res => stml.Variable("var_transformed", res));

            stml.Operations.Add<Streamlet.Op.CallStreamlet>(opt => opt.Operation.Call("stml_02", stml.Variable("var_transformed")))
                .Result<StmlOutputDto>(res => stml.Variable("var_output", res));

            stml.CatchExceptions.Add<InvalidOperationException>(opt => stml.Variable("var_output", new StmlOutputDto { Error = opt.Exception }));
        }
    )
    .CreateStreamlet<OutputDto, StmlOutputDto>(
        "stml_02",
        stml =>
        {
            stml.MapInputToVariable("var_input");
            stml.MapOutputToVariable("var_output");

            stml.Operations.Add<ISomeOperation>(opt => opt.Operation.Get(stml.Variable("var_input")))
                .Result<StmlOutputDto>(res => stml.Variable("var_output", res));

            stml.CatchExceptions.Add<InvalidOperationException>(opt => stml.Variable("var_output", new StmlOutputDto { Error = opt.Exception }));
        }
    )
    .Build();
```

Пример использования:

```
var stml = factory.GetStreamlet<InputDto, OutputDto>("stml_01");
stml.Run(new InputDto());
var result = stml.Result();
```