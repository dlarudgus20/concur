using System;
using System.Threading;

namespace p02
{
    static class Program
    {
        static void Main(string[] args)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            var wm1 = new Windmill_3(x, y, 15);
            var wm2 = new Windmill_3(x + 1, y, 10);

            bool yet1 = true;
            bool yet2 = true;
            for (int tick = 0; yet1 || yet2; ++tick)
            {
                if (tick % 3 == 0)
                    yet1 = wm1.Resume();
                if (tick % 5 == 0)
                    yet2 = wm2.Resume();
                Thread.Sleep(100);
            }
        }
    }
}
