using System;
using System.Collections.Generic;

namespace p17
{
    static class Util
    {
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

        public static Promise SleepPromise(int ms)
        {
            var p = new Promise();
            Scheduler.SleepAsync(ms, () => p.Resolve());
            return p;
        }

        public static Promise KbhitPromise()
        {
            var p = new Promise();
            Scheduler.KbhitAsync(() => p.Resolve());
            return p;
        }
    }
}
