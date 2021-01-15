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
            Console.WriteLine($"AccumulatorForTerminatingProgram: {AccumulatorForTerminatingProgram()}");
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

        public long AccumulatorForTerminatingProgram()
        {
            var gc = new GameConsole(_data);
            for (int i=0; i < gc.Instructions.Count; i++)
            {
                var currInst = gc.Instructions[i];
                var currOp = currInst.Operation;
                if (currOp == Operation.jmp || currOp == Operation.nop)
                {
                    FlipOp(gc, i);
                    gc.Reset();
                    if (DoesProgramTerminate(gc))
                        return gc.Accumulator;
                    FlipOp(gc, i);
                }
            }
            throw new Exception("No program found that terminates");
        }

        private bool DoesProgramTerminate(GameConsole gc)
        {
            var pcs = new HashSet<int>();
            while (true)
            {
                if (pcs.Contains(gc.ProgramCounter))
                    return false;
                pcs.Add(gc.ProgramCounter);
                gc.Step();
                if (gc.IsTerminated)
                    return true;
            }
        }

        private void FlipOp(GameConsole gc, int index)
        {
            var op = gc.Instructions[index].Operation;
            var newOp = op switch
            {
                Operation.nop => Operation.jmp,
                Operation.jmp => Operation.nop,
                _ => throw new Exception($"Unexpected operation: {op}")
            };
            gc.Instructions[index].Operation = newOp;
        }
    }
}
