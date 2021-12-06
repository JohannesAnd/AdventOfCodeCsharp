namespace AdventOfCode2021;

public class Line
{
    private readonly int _x0;
    private readonly int _y0;

    private readonly int _deltaX;
    private readonly int _deltaY;
    private readonly int _length;
    public readonly bool IsDiagonal;

    public Line(string input)
    {
        var parts = input.Split(new[] { ' ', ',' });

        _x0 = int.Parse(parts[0]);
        _y0 = int.Parse(parts[1]);
        var x1 = int.Parse(parts[3]);
        var y1 = int.Parse(parts[4]);

        _deltaX = (_x0 - x1);
        _deltaY = (_y0 - y1);
        IsDiagonal = Math.Abs(_deltaX) > 0 && Math.Abs(_deltaY) > 0;
        _length = Math.Max(Math.Abs(_deltaX), Math.Abs(_deltaY)) + 1;
    }

    public IEnumerable<string> GetPoints()
    {
        return Enumerable.Range(0, _length)
            .Select(index => $"{_x0 - index * Math.Sign(_deltaX)}:{_y0 - index * Math.Sign(_deltaY)}");
    }
}

public class Day5 : Day
{
    private readonly Line[] _lines;

    public Day5()
    {
        _lines = LinesStrings
            .Select(line => new Line(line))
            .ToArray();
    }

    public int Part1()
    {
        return _lines
            .Where(line => !line.IsDiagonal)
            .SelectMany(line => line.GetPoints())
            .GroupBy(line => line)
            .Count(grouping => grouping.Count() > 1);
    }

    public int Part2()
    {
        return _lines
            .SelectMany(line => line.GetPoints())
            .GroupBy(line => line)
            .Count(grouping => grouping.Count() > 1);
    }
}