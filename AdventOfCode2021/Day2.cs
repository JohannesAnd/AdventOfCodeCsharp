using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public enum Direction
    {
        Up,
        Down,
        Forward
    }

    public class Instruction
    {
        public readonly Direction Direction;
        public readonly int Value;

        public Instruction(string input)
        {
            var map = new Dictionary<string, Direction>
            {
                {"forward", Direction.Forward},
                {"up", Direction.Up},
                {"down", Direction.Down}
            };
            var inputSplit = input.Split(' ');

            Direction = map[inputSplit[0]];
            Value = int.Parse(inputSplit[1]);
        }
    }

    public class Day2 : Day
    {
        private readonly Instruction[] _instructions;

        public Day2()
        {
            _instructions = LinesStrings.Select(line => new Instruction(line)).ToArray();
        }

        public int Part1()
        {
            var position = 0;
            var depth = 0;

            foreach (var instruction in _instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Forward:
                    {
                        position += instruction.Value;
                        break;
                    }
                    case Direction.Up:
                    {
                        depth -= instruction.Value;
                        break;
                    }
                    case Direction.Down:
                    {
                        depth += instruction.Value;
                        break;
                    }
                }
            }

            return position * depth;
        }

        public int Part2()
        {
            var position = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in _instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Forward:
                    {
                        position += instruction.Value;
                        depth += aim * instruction.Value;
                        break;
                    }
                    case Direction.Up:
                    {
                        aim -= instruction.Value;
                        break;
                    }
                    case Direction.Down:
                    {
                        aim += instruction.Value;
                        break;
                    }
                }
            }

            return position * depth;
        }
    }
}