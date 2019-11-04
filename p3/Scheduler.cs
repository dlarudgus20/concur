using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace p3
{
    class WaitForTicks
    {
        public int Tick { get; private set; }
        public WaitForTicks(int t)
        {
            Tick = t;
        }
    }

    class Scheduler
    {
        private struct Entry
        {
            public IEnumerator coroutine;
            public int sleepUntil;
        }

        private List<Entry> list = new List<Entry>();
        private int tickCount = 0;

        public void StartCoroutine(IEnumerator cor)
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
                            if (!entry.coroutine.MoveNext())
                            {
                                entry.sleepUntil = tickCount - 1;
                            }
                            else if (entry.coroutine.Current is WaitForTicks obj)
                            {
                                entry.sleepUntil = tickCount + obj.Tick;
                            }
                            else
                            {
                                entry.sleepUntil = tickCount;
                            }
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
