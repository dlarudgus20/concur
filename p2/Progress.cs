using System;

namespace p2
{
    class Progress : ICoroutine
    {
        private readonly int xpos;
        private readonly int ypos;
        private readonly int size;
        private readonly int cycleTime;
        private int value = 0;

        public Progress(int x, int y, int sz, int cycle)
        {
            xpos = x;
            ypos = y;
            size = sz;
            cycleTime = cycle;
        }

        public int Do()
        {
            if (value <= size)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write("|");
                for (int i = 0; i < size; ++i)
                {
                    if (i < value)
                        Console.Write("=");
                    else if (i == value)
                        Console.Write(">");
                    else
                        Console.Write(" ");
                }
                Console.Write("|");

                if (value++ == size)
                    return -1;
                else
                    return cycleTime;
            }
            else
            {
                throw new InvalidOperationException("Coroutine is finished");
            }
        }
    }
}
