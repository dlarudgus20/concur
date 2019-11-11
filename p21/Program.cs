using System;
using System.Threading.Tasks;

namespace p21
{
    static class Program
    {
        static void Main(string[] args)
        {
            AsyncJob1().ContinueWith(task => {
                Console.WriteLine("Job1 Done");
                return AsyncJob2();
            }).Unwrap().ContinueWith(task => {
                Console.WriteLine("Job2 Done");
            });
            Scheduler.MainLoop();
        }

        static Task AsyncJob1()
        {
            Console.WriteLine("input one character");
            return GetchAsync().ContinueWith(task => {
                Console.WriteLine("input: {0}", task.Result);
                return SleepAsync(1000);
            }).Unwrap();
        }

        static async Task AsyncJob2()
        {
            Console.WriteLine("input one character");
            char ch = await GetchAsync();
            Console.WriteLine("input {0}", ch);
            await SleepAsync(1000);
        }

        static Task SleepAsync(int ms)
        {
            var tcs = new TaskCompletionSource<int>();
            Scheduler.SleepAsync(ms, () => {
                tcs.SetResult(0);
            });
            return tcs.Task;
        }

        static Task<char> GetchAsync()
        {
            var tcs = new TaskCompletionSource<char>();
            Scheduler.KbhitAsync(() => {
                char ch = Console.ReadKey(true).KeyChar;
                tcs.SetResult(ch);
            });
            return tcs.Task;
        }
    }
}
