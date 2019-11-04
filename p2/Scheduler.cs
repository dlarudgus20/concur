using System;
using System.Collections.Generic;
using System.Threading;

namespace p2
{
    interface ICoroutine
    {
        int Do();
    }

    class Scheduler
    {
        private struct Entry
        {
            public ICoroutine coroutine;
            public int sleepUntil;
        }

        private List<Entry> list = new List<Entry>();
        private int tickCount = 0;

        public void StartCoroutine(ICoroutine cor)
        {
            list.Add(new Entry() { coroutine = cor, sleepUntil = tickCount });
        }

        public void MainLoop()
        {
            tickCount = 0;
            while (list.Count != 0)
            {
                do
                {
                    for (int idx = 0; idx < list.Count; ++idx)
                    {
                        var entry = list[idx];
                        if (entry.sleepUntil <= tickCount)
                        {
                            entry.sleepUntil = tickCount + entry.coroutine.Do();
                        }
                        list[idx] = entry;
                    }
                    list.RemoveAll(entry => entry.sleepUntil < tickCount);
                } while (list.Exists(entry => entry.sleepUntil == tickCount));

                Thread.Sleep(100);
                tickCount++;
            }
        }
    }
}
