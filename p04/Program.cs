using System;
using System.Collections;
using System.Threading;

namespace p04
{
    static class Program
    {
        static void Main(string[] args)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            IEnumerator wm1 = Windmill_5(x, y, 15);
            IEnumerator wm2 = Windmill_5(x + 1, y, 10);
            IEnumerator pg1 = Progress(x + 8, y, 42).GetEnumerator();

            bool yet1 = true;
            bool yet2 = true;
            bool yet3 = true;
            for (int tick = 0; yet1 || yet2 || yet3; ++tick)
            {
                if (tick % 3 == 0)
                    yet1 = wm1.MoveNext();
                if (tick % 5 == 0)
                    yet2 = wm2.MoveNext();

                yet3 = pg1.MoveNext();
                if (yet3)
                {
                    Console.SetCursorPosition(x + 4, y);
                    Console.Write("{0,2}: ", pg1.Current);
                }

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

        static IEnumerable Progress(int x, int y, int size)
        {
            for (int n = 0; n <= size; ++n)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("|");
                for (int i = 0; i < size; ++i)
                {
                    if (i < n)
                        Console.Write("=");
                    else if (i == n)
                        Console.Write(">");
                    else
                        Console.Write(" ");
                }
                Console.Write("|");
                yield return n;
            }
        }
    }
}
