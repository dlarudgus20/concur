using System;

namespace p15
{
    static class Program
    {
        static void Main(string[] args)
        {
            SomethingAsync().Then(() => {
                Console.WriteLine("Async Done");
            });

            Scheduler.MainLoop();
        }

        static Promise SomethingAsync()
        {
            Console.WriteLine("async1");
            return SleepPromise(1000).Then(() => {
                Console.WriteLine("async2");
                return SleepPromise(1000);
            }).Then(() => {
                Console.WriteLine("async3");
                return SleepPromise(1000);
            }).Then(() => {
                Console.WriteLine("async4");
                return SleepPromise(1000);
            }).Then(() => {
                Console.WriteLine("async5");
                return SleepPromise(1000);
            });
        }

        static Promise SleepPromise(int ms)
        {
            var p = new Promise();
            Scheduler.SleepAsync(ms, () => p.Resolve());
            return p;
        }
    }
}
