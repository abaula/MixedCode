using System;
using System.Linq;

internal class Program
{
    private static void Main()
    {
        var payloads = Enumerable.Range(1, 1000000).ToArray();
        RunParralelJobBatch(payloads, Square, 10).GetAwaiter().GetResult();
    }

    private static Task Square(int value)
    {
        Console.WriteLine($"square of {value} = {value * value}");

        return Task.CompletedTask;
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