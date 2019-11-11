using System;
using System.Collections.Generic;
using System.Threading;

namespace p18
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input four characters");
            AsyncJob1().Then(() => {
                Console.WriteLine("Job1 Done");
                return Util.RunCoroutineAsync(AsyncJob2());
            }).Then(rs => {
                Console.WriteLine("Result: {0}", rs);
                Console.WriteLine("Job2 Done");
            });

            Scheduler.MainLoop();
        }

        static Promise AsyncJob1()
        {
            return GetchAsync().Then(ch => {
                Console.WriteLine("first character: '{0}'", ch);
                return GetchAsync();
            }).Then(ch => {
                Console.WriteLine("second character: '{0}'", ch);
                return GetchAsync();
            }).Then(ch => {
                Console.WriteLine("third character: '{0}'", ch);
                return GetchAsync();
            }).Then(ch => {
                Console.WriteLine("forth character: '{0}'", ch);
            });
        }

        static IEnumerator<Promise<int>> AsyncJob2()
        {
            Console.Write("Calculating.");
            yield return Util.SleepPromise(1000).OfType<int>();
            Console.Write(".");
            yield return Util.SleepPromise(1000).OfType<int>();
            Console.Write(".");
            yield return Util.SleepPromise(1000).OfType<int>();
            Console.WriteLine();
            yield return Promise<int>.ResolvedWith(42);
        }

        static Promise<char> GetchAsync()
        {
            var p = new Promise<char>();
            Scheduler.KbhitAsync(() => {
                char ch = Console.ReadKey(true).KeyChar;
                p.Resolve(ch);
            });
            return p;
        }
    }
}
