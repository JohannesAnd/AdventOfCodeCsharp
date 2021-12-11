namespace AdventOfCode2021;

public class Day9 : Day
{
    private readonly int[][] _map;

    public Day9()
    {
        _map = LinesStrings
            .Select(val => val
                .ToCharArray()
                .Select(val => int.Parse(val.ToString()))
                .ToArray())
            .ToArray();
    }

    private int getValue(int x, int y)
    {
        if (x < 0 || y < 0 || y >= _map.Length || x >= _map[0].Length)
        {
            return -1;
        }

        return _map[y][x];
    }

    private IEnumerable<int> getNeighborValues(int x, int y)
    {
        return new[]
        {
            getValue(x - 1, y),
            getValue(x + 1, y),
            getValue(x, y - 1),
            getValue(x, y + 1)
        }.Where(val => val != -1);
    }

    public long Part1()
    {
        return _map
            .SelectMany((row, y) => row
                .Where((val, x) => getNeighborValues(x, y).All(nb => val < nb)))
            .Select(val => val + 1)
            .Sum();
    }

    public long Part2()
    {
        return -1;
    }
}