using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day13 : DayBase, IDay
    {
        private readonly IList<string> _data;

        public Day13(string filename)
        {
            _data = TextFileLines(filename).ToList();
        }

        public Day13() : this("Day13.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(EarliestBusProduct)}: {EarliestBusProduct()}");
            Console.WriteLine($"{nameof(SynchDepatureTime)}: {SynchDepatureTime()}");
        }

        public int EarliestBusProduct()
        {
            var shortestWait = int.MaxValue;
            var shortestWaitId = -1;
            (var startTime, var ids) = ParseData();
            foreach (var idEntry in ids)
            {
                if (!idEntry.HasValue)
                    continue;
                var id = idEntry.Value;
                var wait = (int)(id - startTime % id);
                if (wait < shortestWait)
                {
                    shortestWait = wait;
                    shortestWaitId = id;
                }
            }
            return shortestWait * shortestWaitId;
        }

        public long SynchDepatureTime()
        {
            (_, var ids) = ParseData();
            var offset = 0;
            var minTime = 0L;
            var canidateTimeInc = 1L;
            foreach(var idEntry in ids)
            {
                if (idEntry.HasValue)
                {
                    int id = idEntry.Value;
                    var newTime = minTime;
                    while (newTime % id != PosMod(id-offset, id))
                        newTime += canidateTimeInc;
                    minTime = newTime;
                    canidateTimeInc *= id;
                }
                offset++;
            }

            return minTime;
        }

        private (int EarliestTime, IList<int?> Ids) ParseData()
        {
            var time = int.Parse(_data[0]);
            var ids = new List<int?>();
            foreach (var id in _data[1].Split(','))
            {
                if (id == "x")
                    ids.Add(null);
                else
                    ids.Add(int.Parse(id));
            }
            return (time, ids);
        }

        // Modulous operator that always returns a positive value.  Needed 
        // because -1%2 returns -1 in C#.
        private int PosMod(int dividend, int divisor)
            => (dividend % divisor + divisor) % divisor;
    }
}
