using System;

namespace p2
{
    class Windmill_3 : ICoroutine
    {
        private const string str = "\\|/-";
        private readonly int xpos;
        private readonly int ypos;
        private readonly int cycleTick;
        private readonly int count;
        private int state = 0;
        private int n = 0;

        public Windmill_3(int x, int y, int tick, int cnt)
        {
            xpos = x;
            ypos = y;
            cycleTick = tick;
            count = cnt;
        }

        public int Do()
        {
            if (n == count)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write(str[state]);
                n++;
                return -1;
            }
            else if (n < count)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write(str[state]);
                state = (state + 1) % str.Length;
                n++;
                return cycleTick;
            }
            else
            {
                throw new InvalidOperationException("Coroutine is finished");
            }
        }
    }
}
