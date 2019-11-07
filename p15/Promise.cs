using System;
using System.Collections.Generic;

namespace p15
{
    sealed class Promise
    {
        private List<(Promise, Func<Promise>)> resolveHandlers = new List<(Promise, Func<Promise>)>();
        private Promise pendingPromise = null;

        private bool resolved = false;

        public static readonly Promise Resolved;

        static Promise()
        {
            Resolved = new Promise();
            Resolved.Resolve();
        }

        public void Resolve()
        {
            if (!resolved)
            {
                resolved = true;

                pendingPromise?.RemoveHandler(this);
                pendingPromise = null;

                foreach (var (next, callback) in resolveHandlers)
                {
                    var p = callback();
                    if (p.resolved)
                    {
                        next.pendingPromise = null;
                        next.Resolve();
                    }
                    else
                    {
                        next.pendingPromise = p;
                        p.resolveHandlers.Add((next, () => Resolved));
                    }
                }
                resolveHandlers.Clear();
            }
        }

        private void RemoveHandler(Promise next)
        {
            int idx = resolveHandlers.FindIndex(tuple => tuple.Item1 == next);
            if (idx != -1)
            {
                resolveHandlers.RemoveAt(idx);
            }
        }

        public Promise Then(Func<Promise> callback)
        {
            if (resolved)
            {
                return callback();
            }
            else
            {
                var next = new Promise();
                resolveHandlers.Add((next, callback));
                next.pendingPromise = this;
                return next;
            }
        }

        public Promise Then(Action callback)
        {
            return Then(() => {
                callback();
                return Resolved;
            });
        }
    }
}
