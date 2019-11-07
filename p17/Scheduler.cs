using System;
using System.Collections.Generic;
using System.Threading;

namespace p17
{
    static class Scheduler
    {
        private static List<Action> kbhits = new List<Action>();
        private static List<(int, Action)> sleepers = new List<(int, Action)>();
        private static int time = 0;
        private static bool stopped = false;

        public static void Stop()
        {
            stopped = true;
        }

        public static void MainLoop()
        {
            while (kbhits.Count > 0 || sleepers.Count > 0)
            {
                if (Console.KeyAvailable && kbhits.Count > 0)
                {
                    var callback = kbhits[0];
                    kbhits.RemoveAt(0);
                    callback();
                    if (stopped)
                        return;
                }

                int idx;
                for (idx = 0; idx < sleepers.Count; ++idx)
                {
                    var (until, callback) = sleepers[idx];
                    if (until <= time)
                    {
                        sleepers.RemoveAt(idx--);
                        callback();
                        if (stopped)
                            return;
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

        public static void KbhitAsync(Action callback)
        {
            kbhits.Add(callback);
        }
    }
}
