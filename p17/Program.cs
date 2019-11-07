using System;
using System.Collections.Generic;
using System.Threading;

namespace p17
{
    static class Program
    {
        static int initialY = 0;
        static int playerY = 0;
        static int point = 0;

        static void Main(string[] args)
        {
            initialY = Console.CursorTop;

            Util.RunCoroutineAsync(PlayerLogic());
            Util.RunCoroutineAsync(StoneLogic());

            Scheduler.MainLoop();
        }

        static IEnumerator<Promise> PlayerLogic()
        {
            int prevY = playerY;
            while (true)
            {
                Console.SetCursorPosition(0, initialY + prevY);
                Console.Write(" ");
                Console.SetCursorPosition(0, initialY + playerY);
                Console.Write(">");
                prevY = playerY;

                yield return Util.KbhitPromise();
                char ch = Console.ReadKey(true).KeyChar;

                if (ch == 's' || ch == 'S')
                {
                    playerY = playerY < 5 ? playerY + 1 : 5;
                }
                else if (ch =='w' || ch == 'W')
                {
                    playerY = playerY > 0 ? playerY - 1 : 0;
                }
            }
        }

        static IEnumerator<Promise> StoneLogic()
        {
            var stones = new (int, int)[12];
            var random = new Random();
            bool defeated = false;

            for (int idx = 0; idx < stones.Length; ++idx)
            {
                stones[idx] = (random.Next(10, 20), random.Next(0, 5));
            }

            while (true)
            {
                Console.SetCursorPosition(2, initialY + 6);
                Console.Write("point: {0,8}", point);

                if (defeated)
                {
                    Console.SetCursorPosition(2, initialY + 7);
                    Console.Write("D E F E A T E D");
                    Thread.Sleep(2000);
                    Scheduler.Stop();
                }

                foreach (var (x, y) in stones)
                {
                    Console.SetCursorPosition(x, initialY + y);
                    Console.Write("*");
                }

                yield return Util.SleepPromise(50);
                point++;

                for (int idx = 0; idx < stones.Length; ++idx)
                {
                    var (x, y) = stones[idx];
                    Console.SetCursorPosition(x, initialY + y);
                    Console.Write(" ");

                    if (x == 1)
                    {
                        if (y == playerY)
                            defeated = true;
                        else
                            stones[idx] = (random.Next(20, 40), random.Next(0, 5));
                    }
                    else
                    {
                        stones[idx] = (x - 1, y);
                    }
                }
            }
        }
    }
}
