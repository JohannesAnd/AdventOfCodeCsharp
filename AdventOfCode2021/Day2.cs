using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public enum DIRECTION
    {
        UP,
        DOWN,
        FORWARD
    }

    public class Instruction
    {
        public readonly DIRECTION Direction;
        public readonly int Value;

        public Instruction(string input)
        {
            var map = new Dictionary<string, DIRECTION>
            {
                {"forward", DIRECTION.FORWARD},
                {"up", DIRECTION.UP},
                {"down", DIRECTION.DOWN}
            };
            var inputSplit = input.Split(' ');

            Direction = map[inputSplit[0]];
            Value = int.Parse(inputSplit[1]);
        }
    }

    public class Day2 : Day
    {
        private readonly Instruction[] _instructions;

        public Day2() : base("day2.txt")
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
                    case DIRECTION.FORWARD:
                    {
                        position += instruction.Value;
                        break;
                    }
                    case DIRECTION.UP:
                    {
                        depth -= instruction.Value;
                        break;
                    }
                    case DIRECTION.DOWN:
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
                    case DIRECTION.FORWARD:
                    {
                        position += instruction.Value;
                        depth += aim * instruction.Value;
                        break;
                    }
                    case DIRECTION.UP:
                    {
                        aim -= instruction.Value;
                        break;
                    }
                    case DIRECTION.DOWN:
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