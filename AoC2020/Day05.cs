using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day05 : DayBase, IDay
    {
        private readonly IEnumerable<string> _data;

        public Day05(string filename)
        {
            _data = TextFileLines(filename);
        }

        public Day05() : this("Day05.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"MaxSeatID: {MaxSeatID()}");
            Console.WriteLine($"MySeatID: {MySeatID()}");
        }

        public int MaxSeatID()
        {
            var result = 0;
            foreach (var pass in _data)
                result = Math.Max(result, SeatID(pass));
            return result;
        }

        public int MySeatID()
        {
            var idSet = new HashSet<int>();
            foreach (var pass in _data)
                idSet.Add(SeatID(pass));
            for (int curr = 1; curr < 1026; curr++)
                if (idSet.Contains(curr - 1) && !idSet.Contains(curr) && idSet.Contains(curr + 1))
                    return curr;
            throw new Exception("Seat not found!");
        }

        private int SeatID(string pass)
        {
            var curr = 1 << 9; // 512, 2^^9
            var result = 0;
            foreach (char c in pass)
            {
                if (c == 'B' || c == 'R')
                    result += curr;
                curr /= 2;
            }
            return result;
        }
    }
}
