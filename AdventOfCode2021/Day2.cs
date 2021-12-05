namespace AdventOfCode2021;

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
        var inputSplit = input.Split(' ');

        Value = int.Parse(inputSplit[1]);
        Direction = inputSplit[0] switch
        {
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            "down" => Direction.Down,
            _ => throw new ArgumentException("Invalid direction")
        };
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
            Action handler = instruction.Direction switch
            {
                Direction.Forward => () => position += instruction.Value,
                Direction.Up => () => depth -= instruction.Value,
                Direction.Down => () => depth += instruction.Value,
                _ => () => { }
            };

            handler();
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
            Action handler = instruction.Direction switch
            {
                Direction.Forward => () =>
                {
                    position += instruction.Value;
                    depth += aim * instruction.Value;
                },
                Direction.Up => () => aim -= instruction.Value,
                Direction.Down => () => aim += instruction.Value,
                _ => () => { }
            };

            handler();
        }

        return position * depth;
    }
}