namespace AdventOfCode2021;

public class Day1 : Day
{
    public int Part1()
    {
        return Solve(1);
    }

    public int Part2()
    {
        return Solve(3);
    }

    private int Solve(int n)
    {
        var length = LinesInts.Length - n;

        var values = Enumerable.Range(0, length + 1)
            .Select((index) => LinesInts.Skip(index).Take(n).Sum())
            .ToArray();

        return Enumerable.Range(1, length)
            .Count((index) => values[index] > values[index - 1]);
    }
}