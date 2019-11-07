using System;
using System.Collections.Generic;

namespace p16
{
    static class Program
    {
        static void Main(string[] args)
        {
            RunCoroutineAsync(SomethingAsyncCoroutine())
            .Then(() => {
                Console.WriteLine("Async Done");
            });

            Scheduler.MainLoop();
        }

        static IEnumerator<Promise> SomethingAsyncCoroutine()
        {
            Console.WriteLine("async1");
            yield return SleepPromise(1000);
            Console.WriteLine("async2");
            yield return SleepPromise(1000);
            Console.WriteLine("async3");
            yield return SleepPromise(1000);
            Console.WriteLine("async4");
            yield return SleepPromise(1000);
            Console.WriteLine("async5");
            yield return SleepPromise(1000);
        }

        public static Promise RunCoroutineAsync(IEnumerator<Promise> coroutine)
        {
            return RunCoroutineAsync(Promise.Resolved, coroutine);
        }

        public static Promise RunCoroutineAsync(Promise prev, IEnumerator<Promise> coroutine)
        {
            return prev.Then(() => {
                if (coroutine.MoveNext())
                {
                    return RunCoroutineAsync(coroutine.Current, coroutine);
                }
                else
                {
                    return Promise.Resolved;
                }
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
