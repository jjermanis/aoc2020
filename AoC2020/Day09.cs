using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day09 : DayBase, IDay
    {
        private readonly IList<long> _data;

        public Day09(string filename)
        {
            _data = TextFileLines(filename).Select(x => long.Parse(x)).ToList();
        }

        public Day09() : this("Day09.txt")
        {
        }

        public void Do()
        {
            var invalidNum = InvalidNumber();
            Console.WriteLine($"InvalidNumber: {invalidNum}");
            Console.WriteLine($"EncryptionWeakness: {EncryptionWeakness(invalidNum)}");
        }

        public long InvalidNumber(int preambleLen=25)
        {
            var recentNumList = new List<long>();
            var recentNumSet = new HashSet<long>();

            for (int i=0; i < preambleLen; i++)
            {
                var val = _data[i];
                recentNumList.Add(val);
                recentNumSet.Add(val);
            }
            
            for (int i= preambleLen; i < _data.Count; i++)
            {
                var val = _data[i];
                var isPairFound = false;
                foreach (var x in recentNumList)
                    if (recentNumSet.Contains(val-x))
                    {
                        isPairFound = true;
                        break;
                    }
                if (!isPairFound)
                    return val;

                var oldVal = recentNumList[0];
                recentNumList.RemoveAt(0);
                recentNumSet.Remove(oldVal);
                recentNumList.Add(val);
                recentNumSet.Add(val);
            }
            throw new Exception("All numbers follow the rule");
        }

        // 16_738_588_017_308 is too high
        //  9_088_260_263_956 is too high
        // 18_473_747_412_840 is too high
        public long EncryptionWeakness(long invalidNum)
        {
            for (int x = 0; x < _data.Count; x++)
            {
                var sum = 0L;
                var min = _data[x];
                var max = _data[x];
                for (int y = x; y < _data.Count; y++)
                {
                    sum += _data[y];
                    min = Math.Min(min, _data[y]);
                    max = Math.Max(max, _data[y]);
                    if (sum == invalidNum)
                        return min + max;
                    if (sum > invalidNum)
                        break;
                }
            }
            throw new Exception("Answer not found");
        }
    }
}
