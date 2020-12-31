using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day03 : DayBase, IDay
    {
        private readonly List<string> _map;

        public Day03(string filename)
        {
            _map = TextFileLines(filename).ToList();
        }

        public Day03() : this("Day03.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"SimpleTreeCount: {SimpleTreeCount()}");
            Console.WriteLine($"TreeCountProduct: {TreeCountProduct()}");
        }

        public long SimpleTreeCount() => TreeCount(1, 3);

        public long TreeCountProduct() =>
            TreeCount(1, 1) * TreeCount(1, 3) * TreeCount(1, 5) * TreeCount(1, 7) * TreeCount(2, 1);

        private long TreeCount(int down, int right)
        {
            var result = 0;
            int x = 0;
            for (var rowIndex = 0; rowIndex < _map.Count; rowIndex+=down)
            {
                var row = _map[rowIndex];
                if (row[x % row.Length] == '#')
                    result++;
                x += right;
            }
            return result;
        }
    }
}
