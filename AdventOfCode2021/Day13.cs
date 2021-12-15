namespace AdventOfCode2021;

public class OrigamiPoint
{
    public int X;
    public int Y;

    public OrigamiPoint(string input)
    {
        var split = input.Split(',');

        X = int.Parse(split[0]);
        Y = int.Parse(split[1]);
    }

    public override string ToString()
    {
        return $"{X}:{Y}";
    }
}

public class OrigamiInstruction
{
    public readonly char Axis;
    public readonly int Value;

    public OrigamiInstruction(string input)
    {
        var split = input.Split(' ')[2].Split('=');

        Axis = split[0].ToCharArray()[0];
        Value = int.Parse(split[1]);
    }
}

public class Origami
{
    private readonly OrigamiInstruction[] _instructions;
    private readonly OrigamiPoint[] _points;

    public Origami(string[] input)
    {
        var splitIndex = Array.IndexOf(input, "");

        _points = input[..splitIndex]
            .Select(line => new OrigamiPoint(line))
            .ToArray();
        _instructions = input[(splitIndex + 1)..]
            .Select(line => new OrigamiInstruction(line))
            .ToArray();
    }

    public string Print()
    {
        var width = _points.Select(p => p.X).Max();
        var height = _points.Select(p => p.Y).Max();

        var board = Enumerable
            .Range(0, height + 1)
            .Select(y => Enumerable
                .Range(0, width + 1)
                .Select(x => _points.FirstOrDefault(p => p.Y == y && p.X == x) != null ? 'X' : '_')
                .ToArray())
            .ToArray();


        return string.Join('\n', board.Select(row => new string(row)));
    }

    public void Fold(int n)
    {
        foreach (var instruction in _instructions.Take(n))
        {
            foreach (var point in _points)
            {
                if (instruction.Axis == 'x')
                {
                    if (point.X > instruction.Value)
                    {
                        point.X = instruction.Value - (point.X - instruction.Value);
                    }
                }

                if (instruction.Axis == 'y')
                {
                    if (point.Y > instruction.Value)
                    {
                        point.Y = instruction.Value - (point.Y - instruction.Value);
                    }
                }
            }
        }
    }

    public int GetUniquePoints()
    {
        return _points.Select(p => p.ToString()).GroupBy(l => l).Count();
    }
}

public class Day13 : Day, Parts
{
    public object Part1()
    {
        var origami = new Origami(LinesStrings);

        origami.Fold(1);

        return origami.GetUniquePoints();
    }

    public object Part2()
    {
        var origami = new Origami(LinesStrings);

        origami.Fold(int.MaxValue);
        
        return origami.Print();
    }
}