using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day08 : DayBase, IDay
    {
        private readonly IEnumerable<string> _data;

        public Day08(string filename)
        {
            _data = TextFileLines(filename);
        }

        public Day08() : this("Day08.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"AccumulatorAfterRepeatedInstruction: {AccumulatorAfterRepeatedInstruction()}");
        }

        public long AccumulatorAfterRepeatedInstruction()
        {
            var pcs = new HashSet<int>();
            var result = 0L;
            var gc = new GameConsole(_data);
            while (true)
            {
                if (pcs.Contains(gc.ProgramCounter))
                    return result;
                result = gc.Accumulator;
                pcs.Add(gc.ProgramCounter);
                gc.Step();
            }
        }
    }
}
