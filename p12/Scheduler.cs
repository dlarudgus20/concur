using System;
using System.Collections.Generic;
using System.Threading;

namespace p12
{
    static class Scheduler
    {
        private static List<(int, Action)> sleepers = new List<(int, Action)>();
        private static int time = 0;

        public static void MainLoop()
        {
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

        public static void SleepAsync(int ms, Action callback)
        {
            sleepers.Add((time + ms, callback));
        }
    }
}
