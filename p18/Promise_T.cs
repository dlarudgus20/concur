using System;

namespace p18
{
    sealed class Promise<T> : Promise
    {
        public T Result { get; private set; } = default(T);

        public static Promise<T> ResolvedWith(T value)
        {
            var p = new Promise<T>();
            p.Resolve(value);
            return p;
        }

        public void Resolve(T value)
        {
            Result = value;
            Resolve();
        }

        protected override void ResolveNext(Promise next)
        {
            if (next is Promise<T> n)
                n.Resolve(Result);
            else
                next.Resolve();
        }

        protected override Func<Promise> PendingNextHandler(Promise next)
        {
            if (next is Promise<T> n)
                return () => ResolvedWith(Result);
            else
                return () => Resolved;
        }

        public Promise<TResult> Then<TResult>(Func<T, Promise<TResult>> callback)
        {
            return (Promise<TResult>)ThenCustom(
                () => callback(Result),
                () => new Promise<TResult>());
        }

        public Promise<TResult> Then<TResult>(Func<T, TResult> callback)
        {
            return (Promise<TResult>)ThenCustom(
                () => Promise<TResult>.ResolvedWith(callback(Result)),
                () => new Promise<TResult>());
        }

        public Promise Then(Func<T, Promise> callback)
        {
            return Then(() => callback(Result));
        }

        public Promise Then(Action<T> callback)
        {
            return Then(() => {
                callback(Result);
                return Resolved;
            });
        }
    }
}
