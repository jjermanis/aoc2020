using System;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = Environment.TickCount;

            new Day02().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}
