using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class GameConsole
    {
        public GameConsole(IEnumerable<string> data)
        {
            Instructions = new List<Instruction>();
            foreach (var instruction in data)
                Instructions.Add(new Instruction(instruction));

            Reset();
        }

        public IList<Instruction> Instructions { get; set; }
        public long Accumulator { get; set; }
        public int ProgramCounter { get; set; }
        public bool IsTerminated { get => ProgramCounter == Instructions.Count; }

        public void Reset()
        {
            Accumulator = 0;
            ProgramCounter = 0;
        }

        public void Step()
        {
            if (IsTerminated)
                throw new Exception("Program terminated");

            var instruction = Instructions[ProgramCounter];
            switch (instruction.Operation)
            {
                case Operation.acc:
                    Accumulator += instruction.Argument;
                    break;
                case Operation.jmp:
                    // Subtract by one here, since it'll get added back after this block
                    ProgramCounter += instruction.Argument - 1;
                    break;
                case Operation.nop:
                    break;
                default:
                    throw new Exception($"Unexpected op: {instruction.Operation}");
            }
            ProgramCounter++;
        }
    }
    
    public enum Operation
    {
        nop,
        acc,
        jmp
    }

    public class Instruction
    {
        public Instruction(string instruction)
        {
            var tokens = instruction.Split(' ');
            Operation = (Operation)Enum.Parse(typeof(Operation), tokens[0]);
            Argument = int.Parse(tokens[1]);
        }

        public Operation Operation { get; set; }
        public int Argument { get; set; }
    }
}
