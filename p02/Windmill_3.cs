using System;

namespace p02
{
    sealed class Windmill_3
    {
        private const string str = "\\|/-";
        private readonly int xpos;
        private readonly int ypos;
        private readonly int count;
        private int n = 0;

        public Windmill_3(int x, int y, int cnt)
        {
            xpos = x;
            ypos = y;
            count = cnt;
        }

        public bool Resume()
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
