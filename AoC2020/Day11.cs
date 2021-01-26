using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day11 : DayBase, IDay
    {
        private const char OCCUPIED = '#';
        private const char EMPTY = 'L';
        private const char FLOOR = '.';

        private readonly IList<string> _data;

        public Day11(string filename)
        {
            _data = TextFileLines(filename).ToList();
        }

        public Day11() : this("Day11.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"StabalizedNeighborSeatCount: {StabalizedNeighborSeatCount()}");
            Console.WriteLine($"StabalizedVisibleSeatCount: {StabalizedVisibleSeatCount()}");
        }

        public int StabalizedNeighborSeatCount() =>
            StabalizedSeatCount(NeighborCount, 4);

        public int StabalizedVisibleSeatCount() =>
            StabalizedSeatCount(VisibleCount, 5);

        private int StabalizedSeatCount(
            Func<Layout, int, int, int> SeatCount,
            int seatLimit)
        {
            var grid = InitLayout();

            while (grid.IsStable != true)
            {
                grid = RunRound(grid, SeatCount, seatLimit);
            }

            var result = 0;
            foreach (var seat in grid.Positions.Values)
                if (seat == OCCUPIED)
                    result++;
            return result;
        }

        private Layout RunRound(
            Layout layout,
            Func<Layout, int, int, int> SeatCount,
            int seatLimit)
        {
            var newLayout = new Layout()
            {
                Positions = new Dictionary<(int, int), char>(),
                Width = layout.Width,
                Height = layout.Height
            };
            bool hasChanged = false;

            foreach (var position in layout.Positions.Keys)
            {
                (var x, var y) = position;
                var curr = layout.Positions[(x, y)];
                if (curr == FLOOR)
                    continue;
                var neighbors = SeatCount(layout, x, y);
                var next = curr switch
                {
                    EMPTY => neighbors == 0 ? OCCUPIED : EMPTY,
                    OCCUPIED => neighbors >= seatLimit ? EMPTY : OCCUPIED,
                    _ => throw new Exception($"Unexpected value: {curr}")
                };
                if (curr != next)
                    hasChanged = true;
                newLayout.Positions.Add((x, y), next);

            }
            newLayout.IsStable = !hasChanged;
            return newLayout;
        }

        private int NeighborCount(Layout layout, int x, int y)
        {
            var result = 0;
            for (int nx = x - 1; nx <= x + 1; nx++)
                for (int ny = y - 1; ny <= y + 1; ny++)
                    if (layout.Positions.ContainsKey((nx, ny)) && 
                        layout.Positions[(nx, ny)] == OCCUPIED && 
                        (nx != x || ny != y))
                        result++;
            return result;
        }

        private int VisibleCount(Layout layout, int x, int y)
        {
            var result = 0;
            for (int vx = -1; vx <= 1; vx++)
                for (int vy = -1; vy <= 1; vy++)
                {
                    if (vx == 0 && vy == 0)
                        continue;

                    var t = 0;
                    while (true)
                    {
                        t++;
                        var nx = x + (t * vx);
                        var ny = y + (t * vy);

                        if (nx < 0 || ny < 0 || nx > layout.Width || ny > layout.Height)
                            break;
                        if (!layout.Positions.ContainsKey((nx, ny)))
                            continue;
                        if (layout.Positions[(nx, ny)] == OCCUPIED)
                            result++;
                        break;
                    }
                }
            return result;
        }

        private void PrintLayout(Layout layout)
        {
            Console.WriteLine("==========");
            for (var y = 0; y < layout.Height; y++)
            {
                for (var x = 0; x < layout.Width; x++)
                {
                    char curr;
                    if (layout.Positions.ContainsKey((x, y)))
                        curr = layout.Positions[(x, y)];
                    else
                        curr = '.';
                    Console.Write(curr);
                }
                Console.WriteLine();
            }
        }

        private Layout InitLayout()
        {
            var layout = new Layout()
            {
                Positions = new Dictionary<(int, int), char>(),
                Width = _data[0].Length,
                Height = _data.Count
            };

            for (var row = 0; row < layout.Height; row++)
                for (var col = 0; col < layout.Width; col++)
                {
                    var curr = _data[row][col];
                    if (curr != FLOOR)
                        layout.Positions[(col, row)] = curr;
                }

            return layout;
        }
    }

    class Layout
    {
        public IDictionary<(int, int), char> Positions { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool? IsStable { get; set; }
    }
}
