using System;
using System.Collections.Generic;

namespace p18
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

        public static Promise<T> RunCoroutineAsync<T>(IEnumerator<Promise<T>> coroutine)
        {
            return RunCoroutineAsync(Promise<T>.ResolvedWith(default(T)), coroutine);
        }

        public static Promise<T> RunCoroutineAsync<T>(Promise<T> prev, IEnumerator<Promise<T>> coroutine)
        {
            return prev.Then(result => {
                if (coroutine.MoveNext())
                {
                    return RunCoroutineAsync(coroutine.Current, coroutine);
                }
                else
                {
                    return Promise<T>.ResolvedWith(result);
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
