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
            Console.WriteLine($"StabalizedSeatCount: {StabalizedSeatCount()}");
        }

        public int StabalizedSeatCount()
        {
            var grid = InitLayout();

            while (grid.IsStable != true)
                grid = RunRound(grid);

            var result = 0;
            foreach (var seat in grid.Positions.Values)
                if (seat == OCCUPIED)
                    result++;
            return result;
        }

        private Layout RunRound(Layout layout)
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
                var neighbors = NeighborCount(layout, x, y);
                var next = curr switch
                {
                    EMPTY => neighbors == 0 ? OCCUPIED : EMPTY,
                    OCCUPIED => neighbors >= 4 ? EMPTY : OCCUPIED,
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
                        layout.Positions[(row, col)] = curr;
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
