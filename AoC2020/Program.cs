﻿using System;

namespace AoC2020
{
    class Program
    {
        static void Main()
        {
            int start = Environment.TickCount;

            new Day05().Do();

            Console.WriteLine($"Time: {Environment.TickCount - start} ms");
        }
    }
}
