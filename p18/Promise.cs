using System;
using System.Collections.Generic;

namespace p18
{
    enum PromiseState
    {
        Pending, Resolved
    }

    class Promise
    {
        private List<(Promise, Func<Promise>)> resolveHandlers = new List<(Promise, Func<Promise>)>();
        private Promise pendingPromise = null;

        public PromiseState State { get; private set; } = PromiseState.Pending;

        public static Promise Resolved { get; private set; }

        static Promise()
        {
            Resolved = new Promise();
            Resolved.Resolve();
        }

        public void Resolve()
        {
            if (State == PromiseState.Pending)
            {
                State = PromiseState.Resolved;

                pendingPromise?.RemoveHandler(this);
                pendingPromise = null;

                foreach (var (next, callback) in resolveHandlers)
                {
                    var p = callback();
                    if (p.State == PromiseState.Resolved)
                    {
                        next.pendingPromise = null;
                        p.ResolveNext(next);
                    }
                    else
                    {
                        next.pendingPromise = p;
                        p.resolveHandlers.Add((next, p.PendingNextHandler(next)));
                    }
                }
                resolveHandlers.Clear();
            }
        }

        protected virtual void ResolveNext(Promise next)
        {
            next.Resolve();
        }

        protected virtual Func<Promise> PendingNextHandler(Promise next)
        {
            return () => Resolved;
        }

        private void RemoveHandler(Promise next)
        {
            int idx = resolveHandlers.FindIndex(tuple => tuple.Item1 == next);
            if (idx != -1)
            {
                resolveHandlers.RemoveAt(idx);
            }
        }

        public Promise ThenCustom(Func<Promise> callback, Func<Promise> creator)
        {
            if (State == PromiseState.Resolved)
            {
                return callback();
            }
            else
            {
                var next = creator();
                resolveHandlers.Add((next, callback));
                next.pendingPromise = this;
                return next;
            }
        }

        public Promise Then(Func<Promise> callback)
        {
            return ThenCustom(callback, () => new Promise());
        }

        public Promise Then(Action callback)
        {
            return Then(() => {
                callback();
                return Resolved;
            });
        }
        public Promise<T> OfType<T>()
        {
            return (Promise<T>)ThenCustom(
                () => Promise.Resolved,
                () => new Promise<T>());
        }

        public Promise<T> Then<T>(Func<Promise<T>> callback)
        {
            return (Promise<T>)ThenCustom(callback, () => new Promise<T>());
        }

        public Promise<T> Then<T>(Func<T> callback)
        {
            return (Promise<T>)ThenCustom(
                () => Promise<T>.ResolvedWith(callback()),
                () => new Promise<T>());
        }
    }
}
