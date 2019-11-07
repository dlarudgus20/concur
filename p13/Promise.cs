using System;

namespace p13
{
    sealed class Promise
    {
        private Action resolveHandler = null;

        private bool resolved = false;

        public void Resolve()
        {
            if (!resolved)
            {
                resolved = true;
                if (resolveHandler != null)
                {
                    resolveHandler();
                }
            }
        }

        public void Then(Action callback)
        {
            if (resolved)
            {
                callback();
            }
            else
            {
                resolveHandler = callback;
            }
        }
    }
}
