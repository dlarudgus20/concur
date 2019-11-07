using System;
using System.Collections;
using System.Threading;

namespace p03
{
    static class Program
    {
        static void Main(string[] args)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            IEnumerator wm1 = new Windmill_4(x, y, 15);
            IEnumerator wm2 = Windmill_5(x + 1, y, 10);

            bool yet1 = true;
            bool yet2 = true;
            for (int tick = 0; yet1 || yet2 || yet3; ++tick)
            {
                if (tick % 3 == 0)
                    yet1 = wm1.MoveNext();
                if (tick % 5 == 0)
                    yet2 = wm2.MoveNext();

                Thread.Sleep(100);
            }
        }

        static IEnumerator Windmill_5(int x, int y, int count)
        {
            string str = "\\|/-";
            for (int n = 0; n < count; ++n)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(str[n % str.Length]);
                yield return null;
            }
        }
    }
}
