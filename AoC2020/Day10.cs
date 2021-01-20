using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day10 : DayBase, IDay
    {
        private readonly List<int> _data;

        private readonly IReadOnlyDictionary<int, int> MULTIPLIER_MAP = new Dictionary<int, int>
        {
            {0, 1},
            {1, 1},
            {2, 2},
            {3, 4},
            {4, 7}
        };

        public Day10(string filename)
        {
            _data = TextFileLines(filename).Select(x => int.Parse(x)).ToList();
            _data.Sort();

            // "your device has a built-in joltage adapter rated for 3 jolts higher than the 
            // highest-rated adapter in your bag" - Add it to the list
            var max = _data[_data.Count - 1];
            _data.Add(max + 3);
        }

        public Day10() : this("Day10.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"JoltageDifferenceProduct: {JoltageDifferenceProduct()}");
            Console.WriteLine($"PossibleCombinations: {PossibleCombinations()}");
        }

        public int JoltageDifferenceProduct()
        {
            var curr = 0;
            var oneCount = 0;
            var threeCount = 0;

            foreach (var x in _data)
            {
                var diff = x - curr;
                switch (x - curr)
                {
                    case 1:
                        oneCount++;
                        break;
                    case 3:
                        threeCount++;
                        break;
                    default:
                        throw new Exception("Unexpected joltage difference");
                }
                curr = x;
            }
            return oneCount * threeCount;
        }

        public long PossibleCombinations()
        {
            var curr = 0;
            var oneStreak = 0;
            var result = 1L;

            foreach (var x in _data)
            {
                var diff = x - curr;
                switch (diff)
                {
                    case 1:
                        oneStreak++;
                        break;
                    case 3:
                        result *= MULTIPLIER_MAP[oneStreak];
                        oneStreak = 0;
                        break;
                    default:
                        throw new Exception("Unexpected joltage difference");
                }
                curr = x;
            }
            return result;
        }
    }
}
