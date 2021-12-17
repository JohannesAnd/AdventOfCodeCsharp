namespace AdventOfCode2021;

public class Launcher
{
    public readonly int MaxX;
    public readonly int MaxY;
    public readonly int MinX;
    public readonly int MinY;

    private readonly int _goalMinX;
    private readonly int _goalMinY;
    private readonly int _goalMaxX;
    private readonly int _goalMaxY;


    public Launcher(string input)
    {
        var parts = input.Split('=', '.', ' ', ',');

        var x = new[] { parts[3], parts[5] }.Select(int.Parse).OrderBy(v => v).ToArray();
        var y = new[] { parts[8], parts[10] }.Select(int.Parse).OrderBy(v => v).ToArray();

        _goalMinY = y[0];
        _goalMaxY = y[1];
        _goalMinX = x[0];
        _goalMaxX = x[1];

        MaxY = -_goalMinY - 1;
        MinY = _goalMinY;
        MaxX = _goalMaxX;
        MinX = 1;
    }

    public int GetMaxHeight()
    {
        return Enumerable.Range(1, MaxY).Sum();
    }

    public bool CheckIfHits(int x, int y)
    {
        var xVelocity = x;
        var yVelocity = y;
        var xPosition = 0;
        var yPosition = 0;

        while (xPosition <= _goalMaxX && yPosition >= _goalMinY)
        {
            xPosition += xVelocity;
            yPosition += yVelocity;
            xVelocity = Math.Max(0, xVelocity - 1);
            yVelocity--;

            if (xPosition >= _goalMinX && xPosition <= _goalMaxX && yPosition <= _goalMaxY && yPosition >= _goalMinY)
            {
                return true;
            }
        }

        return false;
    }
}

public class Day17 : Day, Parts
{
    private readonly Launcher _launcher;

    public Day17()
    {
        _launcher = new Launcher(LinesStrings[0]);
    }

    public object Part1()
    {
        return _launcher.GetMaxHeight();
    }

    public object Part2()
    {
        var count = 0;

        for (var x = _launcher.MinX; x <= _launcher.MaxX; x++)
        {
            for (var y = _launcher.MinY; y <= _launcher.MaxY; y++)
            {
                if (_launcher.CheckIfHits(x, y))
                {
                    count++;
                }
            }
        }

        return count;
    }
}