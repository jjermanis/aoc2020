using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day02 : DayBase, IDay
    {
        private readonly IEnumerable<string> _passwordSummary;

        public Day02(string filename)
        {
            _passwordSummary = TextFileLines(filename);
        }

        public Day02() : this("Day02.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"ValidPasswordCount: {ValidPasswordCount()}");
            Console.WriteLine($"ValidPasswordCountV2: {ValidPasswordCountV2()}");
        }

        public int ValidPasswordCount()
        {
            var result = 0;
            foreach(var summary in _passwordSummary)
            {
                var tokens = summary.Split(' ');
                var policyRange = tokens[0].Split('-').Select(x => int.Parse(x)).ToList();
                var policyChar = tokens[1][0];
                var password = tokens[2];

                var charCount = password.Count(c => c == policyChar);
                if (charCount >= policyRange[0] &&
                    charCount <= policyRange[1])
                    result++;
            }
            return result;
        }

        public int ValidPasswordCountV2()
        {
            var result = 0;
            foreach (var summary in _passwordSummary)
            {
                var tokens = summary.Split(' ');
                var policyIndexes = tokens[0].Split('-').Select(x => int.Parse(x)).ToList();
                var policyChar = tokens[1][0];
                var password = tokens[2];

                if (password[policyIndexes[0] - 1] == policyChar ^
                    password[policyIndexes[1] - 1] == policyChar)
                    result++;
            }
            return result;
        }
    }
}
