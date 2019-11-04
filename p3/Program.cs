using System;

namespace p3
{
    class Program
    {
        static void Main(string[] args)
        {
            var sched = new Scheduler();
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            sched.StartCoroutine(new Windmill_4(x, y, 2, 105));
            sched.StartCoroutine(new Windmill_4(x + 1, y, 3, 70));
            sched.StartCoroutine(new Windmill_4(x + 2, y, 5, 42));
            sched.StartCoroutine(new Windmill_4(x + 3, y, 7, 30));
            sched.StartCoroutine(new Progress(x + 7, y, 42, 5));
            sched.MainLoop();
        }
    }
}
