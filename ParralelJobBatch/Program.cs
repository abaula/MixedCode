using System;
using System.Threading.Tasks;
using System.Linq;

internal class Program
{
    private static Random _random = new Random();

    private static void Main()
    {
        var payloads = Enumerable.Range(1, 20).ToArray();
        RunParralelJobBatch(payloads, Square, 10).GetAwaiter().GetResult();
    }

    private static async Task Square(int value)
    {
        var threadId1 = Thread.CurrentThread.ManagedThreadId;
        await Task.Delay(TimeSpan.FromMilliseconds(_random.Next(1, 100)));
        var threadId2 = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Thread({threadId1}, {threadId2}). Square of {value} = {value * value}");
    }

    private static async Task RunParralelJobBatch<TPayload>(TPayload[] payloads, Func<TPayload, Task> job, int parralelJobsLimit)
    {
        var jobsSet = payloads
            .Take(parralelJobsLimit)
            .Select(_ => Task.Run(() => job(_)))
            .ToHashSet();

        for (var i = parralelJobsLimit; i < payloads.Length; i++)
        {
            var completed = await Task.WhenAny(jobsSet);
            jobsSet.Remove(completed);
            // i не должен попадать внутрь метода. http://en.wikipedia.org/wiki/Closure_(computer_science)
            // иначе кроме неправильных данных получаемых методом, будет выход за границы массива payloads,
            // т.к. цикл for сначала увеличивает значение i на 1, потом делает проверку.
            var loadValue = payloads[i];
            jobsSet.Add(Task.Run(() => job(loadValue)));
        }

        await Task.WhenAll(jobsSet);
    }
}