using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace UnitOfWorkScopesApp
{
    public abstract class ScenarioWork<TArg, TReturn>
    {
        public abstract TReturn Do(TArg arg);
    }
    
    public class Scenario
    {
        public static ScenarioT<TResult> StartWork<TArg, TResult>(TArg arg, ScenarioWork<TArg, TResult> work)
        {
            return new ScenarioT<TResult>
            {
                Scenario = new Scenario(),
                Result = work.Do(arg)
            };
        }
    }

    public class ScenarioT<TResult>
    {
        public Scenario Scenario { get; set; }
        public TResult Result { get; set; }
    }

    public static class ScenarioExtension
    {
        public static ScenarioT<TReturn> ThenWork<TArg, TReturn>(this ScenarioT<TArg> arg, ScenarioWork<TArg, TReturn> work)
        {
            return new ScenarioT<TReturn>
            {
                Scenario = arg.Scenario,
                Result = work.Do(arg.Result),
            };
        }

        public static ScenarioT<TReturn> ThenWorkIf<TArg, TReturn>(this ScenarioT<TArg> arg, ScenarioWork<TArg, TReturn> work, Func<TArg, bool> conditionFunc)
        {
            if (!conditionFunc(arg.Result))
            {
                return new ScenarioT<TReturn>
                {
                    Scenario = arg.Scenario,
                    Result = default(TReturn)
                };
            }

            return new ScenarioT<TReturn>
            {
                Scenario = arg.Scenario,
                Result = work.Do(arg.Result)
            };
        }
    }

    public class MyWork : ScenarioWork<string, Guid>
    {
        public override Guid Do(string arg)
        {
            throw new NotImplementedException();
        }
    }

    public class MyWork2 : ScenarioWork<Guid, int>
    {
        public override int Do(Guid arg)
        {
            throw new NotImplementedException();
        }
    }

    public class MyWork3 : ScenarioWork<int, string>
    {
        public override string Do(int arg)
        {
            throw new NotImplementedException();
        }
    }

    public class ScenarioRunner
    {
        public void Run()
        {
            Scenario
                .StartWork(string.Empty, new MyWork())
                .ThenWork(new MyWork2())
                .ThenWorkIf(new MyWork3(), r => r > 3);
        }

    }


}
