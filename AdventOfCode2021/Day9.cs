namespace AdventOfCode2021;

public class Point
{
    public readonly int X;
    public readonly int Y;
    public readonly int Value;

    public Point(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }
}

public class Day9 : Day
{
    private readonly Point[][] _map;
    private IEnumerable<Point> _lowPoints;

    public Day9()
    {
        _map = LinesStrings
            .Select((line, y) => line
                .ToCharArray()
                .Select((c, x) => new Point(x, y, int.Parse(c.ToString())))
                .ToArray())
            .ToArray();
    }

    private Point GetPoint(int x, int y)
    {
        if (x < 0 || y < 0 || y >= _map.Length || x >= _map[0].Length)
        {
            return null;
        }

        return _map[y][x];
    }

    private IEnumerable<Point> GetNeighbors(Point point)
    {
        return new[]
        {
            GetPoint(point.X - 1, point.Y),
            GetPoint(point.X + 1, point.Y),
            GetPoint(point.X, point.Y - 1),
            GetPoint(point.X, point.Y + 1)
        }.Where(val => val != null);
    }

    private IEnumerable<Point> GetLowPoints()
    {
        _lowPoints = _map
            .SelectMany(row => row
                .Where(point => GetNeighbors(point).All(nb => point.Value < nb.Value)));
        return _lowPoints;
    }

    private long GetBasinSize(Point p)
    {
        var seen = new Queue<Point>(GetNeighbors(p).Where(n => n.Value < 9));
        var visited = new HashSet<Point>() { p };

        while (seen.Count > 0)
        {
            var nextPoint = seen.Dequeue();

            visited.Add(nextPoint);

            var neighbors = GetNeighbors(nextPoint)
                .Where(n => !visited.Contains(n) && !seen.Contains(n) && n.Value < 9);

            foreach (var neighbor in neighbors)
            {
                seen.Enqueue(neighbor);
            }
        }

        return visited.Count;
    }

    private IEnumerable<long> GetBasinSizes()
    {
        return GetLowPoints().Select(GetBasinSize);
    }

    public long Part1()
    {
        return GetLowPoints()
            .Select(point => point.Value + 1)
            .Sum();
    }

    public long Part2()
    {
        var largestBasins = GetBasinSizes().OrderBy(s => s).Reverse().ToArray();

        return largestBasins[0] * largestBasins[1] * largestBasins[2];
    }
}