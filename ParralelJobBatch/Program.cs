using System;
using System.Threading.Tasks;
using System.Linq;

internal class Program
{
    private static Random _random = new Random();

    private static void Main()
    {
        var payloads = Enumerable.Range(1, 1000).ToArray();
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
            .Select(_ => job(_))
            .ToHashSet();

        for (int i = parralelJobsLimit + 1; i < payloads.Length; i++)
        {
            var completed = await Task.WhenAny(jobsSet);
            jobsSet.Remove(completed);
            jobsSet.Add(job(payloads[i]));
        }
    }
}