using System;

namespace p13
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("async1");
            SleepPromise(1000).Then(() => {
                Console.WriteLine("async2");
            });

            Scheduler.MainLoop();
        }

        static Promise SleepPromise(int ms)
        {
            var p = new Promise();
            Scheduler.SleepAsync(ms, () => p.Resolve());
            return p;
        }
    }
}
