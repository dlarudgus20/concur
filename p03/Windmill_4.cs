using System;
using System.Collections;

namespace p03
{
    sealed class Windmill_4 : IEnumerator
    {
        private const string str = "\\|/-";
        private readonly int xpos;
        private readonly int ypos;
        private readonly int count;
        private int n = 0;

        public object Current => null;

        public Windmill_4(int x, int y, int cnt)
        {
            xpos = x;
            ypos = y;
            count = cnt;
        }

        public void Reset()
        {
            n = 0;
        }

        public bool MoveNext()
        {
            if (n < count)
            {
                Console.SetCursorPosition(xpos, ypos);
                Console.Write(str[n % str.Length]);
                n++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
