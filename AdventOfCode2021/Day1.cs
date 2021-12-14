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
        return LinesInts
            .Window(n)
            .Select(values => values.Sum())
            .Window(2)
            .Count(values => values[1] > values[0]);
    }
}