using System;
using System.Collections.Generic;

namespace p14
{
    sealed class Promise
    {
        private List<(Promise, Action)> resolveHandlers = new List<(Promise, Action)>();
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
                    callback();
                    next.pendingPromise = null;
                    next.Resolve();
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

        public Promise Then(Action callback)
        {
            if (resolved)
            {
                callback();
                return Resolved;
            }
            else
            {
                var next = new Promise();
                resolveHandlers.Add((next, callback));
                next.pendingPromise = this;
                return next;
            }
        }
    }
}
