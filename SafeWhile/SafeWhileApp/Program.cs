using System;

namespace SafeWhileSample;

static class Program
{
    public static void Main()
    {
        var limit1 = new Limit(10);

        while (limit1.Next())
            Console.WriteLine("Hello!");

        var limit2 = new Limit(10);

        while (limit2.NextOrThrow())
            Console.WriteLine("Hello thow!");            
    }
}
