using System;
using System.Collections;

namespace p3
{
    class Windmill_4 : IEnumerator
    {
        private const string str = "\\|/-";
        private readonly int xpos;
        private readonly int ypos;
        private readonly int cycleTick;
        private readonly int count;
        private int state = 0;
        private int n = 0;

        public object Current { get; private set; }

        public Windmill_4(int x, int y, int tick, int cnt)
        {
            xpos = x;
            ypos = y;
            cycleTick = tick;
            count = cnt;
        }

        public void Reset()
        {
            state = 0;
            n = 0;
            Current = null;
        }

        public bool MoveNext()
        {
            if (n == count)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write(str[state]);
                n++;
                return false;
            }
            else if (n < count)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write(str[state]);
                state = (state + 1) % str.Length;
                n++;
                Current = new WaitForTicks(cycleTick);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
