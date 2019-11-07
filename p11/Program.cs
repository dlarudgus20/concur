using System;
using System.Collections.Generic;
using System.Threading;

namespace p11
{
    static class Program
    {
        private static List<(int, Action)> sleepers = new List<(int, Action)>();
        private static int time = 0;

        static void Main(string[] args)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            WindmillAsync(x, y, 0, 15, 300);
            WindmillAsync(x + 1, y, 0, 10, 500);
            ProgressAsync(x + 4, y, 0, 50, 100);

            while (sleepers.Count > 0)
            {
                int idx;
                for (idx = 0; idx < sleepers.Count; ++idx)
                {
                    var (until, callback) = sleepers[idx];
                    if (until <= time)
                    {
                        sleepers.RemoveAt(idx--);
                        callback();
                    }
                }

                Thread.Sleep(100);
                time += 100;
            }
        }

        static void SleepAsync(int ms, Action callback)
        {
            sleepers.Add((time + ms, callback));
        }

        static void WindmillAsync(int x, int y, int n, int count, int time)
        {
            string str = "\\|/-";
            if (n < count)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(str[n % str.Length]);
                SleepAsync(time, () => WindmillAsync(x, y, n + 1, count, time));
            }
        }

        static void ProgressAsync(int x, int y, int n, int size, int time)
        {
            if (n <= size)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("|");
                for (int i = 0; i < size; ++i)
                {
                    if (i < n)
                        Console.Write("=");
                    else if (i == n)
                        Console.Write(">");
                    else
                        Console.Write(" ");
                }
                Console.Write("|");
                SleepAsync(time, () => ProgressAsync(x, y,n + 1, size, time));
            }
        }
    }
}
