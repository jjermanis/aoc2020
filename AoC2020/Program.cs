using System;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = Environment.TickCount;

            new Day01().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}
