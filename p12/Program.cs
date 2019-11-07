using System;
using System.Threading;

namespace p12
{
    static class Program
    {
        static void Main(string[] args)
        {
            SomethingSync();
            Console.WriteLine("Sync Done");

            SomethingAsync(() => {
                Console.WriteLine("Async Done");
            });
            Scheduler.MainLoop();
        }

        static void SomethingSync()
        {
            Console.WriteLine("sync1");
            Thread.Sleep(1000);
            Console.WriteLine("sync2");
            Thread.Sleep(1000);
            Console.WriteLine("sync3");
            Thread.Sleep(1000);
            Console.WriteLine("sync4");
            Thread.Sleep(1000);
            Console.WriteLine("sync5");
            Thread.Sleep(1000);
        }

        static void SomethingAsync(Action callback)
        {
            Console.WriteLine("async1");
            Scheduler.SleepAsync(1000, () => {
                Console.WriteLine("async2");
                Scheduler.SleepAsync(1000, () => {
                    Console.WriteLine("async3");
                    Scheduler.SleepAsync(1000, () => {
                        Console.WriteLine("async4");
                        Scheduler.SleepAsync(1000, () => {
                            Console.WriteLine("async5");
                            Scheduler.SleepAsync(1000, callback);
                        });
                    });
                });
            });
        }
    }
}
