using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day12 : DayBase, IDay
    {
        private const char EAST = 'E';
        private const char SOUTH = 'S';
        private const char WEST = 'W';
        private const char NORTH = 'N';
        private const char LEFT = 'L';
        private const char RIGHT = 'R';
        private const char FORWARD = 'F';

        // These are in order to support rotation
        private readonly IList<char> DIRECTIONS = new List<char>
            { EAST, SOUTH, WEST, NORTH };

        private readonly IEnumerable<string> _data;

        public Day12(string filename)
        {
            _data = TextFileLines(filename);
        }

        public Day12() : this("Day12.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(NavigationDistancePart1)}: {NavigationDistancePart1()}");
        }

        public int NavigationDistancePart1()
        {
            var x = 0;
            var y = 0;
            var direction = 0;  // EAST

            foreach (var instruction in _data)
            {
                (var command, var value) = ParseInstruction(instruction);

                switch (command)
                {
                    case EAST:
                    case SOUTH:
                    case WEST:
                    case NORTH:
                        (x, y) = MovePart1(command, value, x, y);
                        break;
                    case LEFT:
                        direction = (4 + direction - (value / 90)) % 4;
                        break;
                    case RIGHT:
                        direction = (4 + direction + (value / 90)) % 4;
                        break;
                    case FORWARD:
                        (x, y) = MovePart1(DIRECTIONS[direction], value, x, y);
                        break;
                    default:
                        throw new Exception($"Unexpected command: {command}");
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private (char Command, int Value) ParseInstruction(string instruction) =>
            (instruction[0], int.Parse(instruction[1..]));

        private (int, int) MovePart1(
            char direction,
            int distance,
            int x, int y)
        {
            switch (direction)
            {
                case EAST:
                    x += distance;
                    break;
                case SOUTH:
                    y -= distance;
                    break;
                case WEST:
                    x -= distance;
                    break;
                case NORTH:
                    y += distance;
                    break;
            }
            return (x, y);
        }
    }
}
