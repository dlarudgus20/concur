using System;
using System.Threading;

namespace p01
{
    static class Program
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
            for (int n = 0; n < count; ++n)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(str[n % str.Length]);
                Thread.Sleep(500);
            }
        }

        static void Windmill_2(int x1, int y1, int c1, int x2, int y2, int c2)
        {
            string str = "\\|/-";
            int n1 = 0;
            int n2 = 0;
            for (int tick = 0; n1 < c1 || n2 < c2; ++tick)
            {
                if (tick % 3 == 0)
                {
                    if (n1 < c1)
                    {
                        Console.SetCursorPosition(x1, y1);
                        Console.Write(str[n1 % str.Length]);
                        n1++;
                    }
                }
                if (tick % 5 == 0)
                {
                    if (n2 < c2)
                    {
                        Console.SetCursorPosition(x2, y2);
                        Console.Write(str[n2 % str.Length]);
                        n2++;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
