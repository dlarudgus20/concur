using System;
using System.Threading;

namespace p1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Windmill_1(x, y, 10);
            Console.WriteLine();
            Windmill_2(x, y, 15, x + 1, y, 10);
        }

        static void Windmill_1(int x, int y, int count)
        {
            string str = "\\|/-";
            int state = 0;
            for (int n = 0; n < count; ++n)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(str[state]);
                state = (state + 1) % str.Length;
                Thread.Sleep(500);
            }
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        static void Windmill_2(int x1, int y1, int c1, int x2, int y2, int c2)
        {
            string str = "\\|/-";
            int state1 = 0;
            int n1 = 0;
            int state2 = 0;
            int n2 = 0;
            for (int tick = 0; ; ++tick)
            {
                if (tick % 3 == 0)
                {
                    if (n1 < c1)
                    {
                        Console.SetCursorPosition(x1, y1);
                        Console.Write(str[state1]);
                        state1 = (state1 + 1) % str.Length;
                        n1++;
                    }
                    else if (n1 == c1)
                    {
                        Console.SetCursorPosition(x1, y1);
                        Console.Write(' ');
                        n1++;
                    }
                }
                if (tick % 5 == 0)
                {
                    if (n2 < c2)
                    {
                        Console.SetCursorPosition(x2, y2);
                        Console.Write(str[state2]);
                        state2 = (state2 + 1) % str.Length;
                        n2++;
                    }
                    else if (n2 == c2)
                    {
                        Console.SetCursorPosition(x2, y2);
                        Console.Write(' ');
                        n2++;
                    }
                }
                if (n1 > c1 && n2 > c2)
                    break;
                Thread.Sleep(100);
            }
        }
    }
}
