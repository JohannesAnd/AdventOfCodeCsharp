namespace AdventOfCode2021;

public class Day6 : Day, Parts
{
    private readonly IEnumerable<long> _startingPopulation;

    public Day6()
    {
        var state = new List<long> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        foreach (var age in LinesStrings[0].Split(',').Select(int.Parse).GroupBy(val => val))
        {
            state[age.Key] = age.Count();
        }

        _startingPopulation = state;
    }

    private long RunNIterations(int n)
    {
        var state = _startingPopulation.ToList();

        for (var i = 0; i < n; i++)
        {
            state[(i + 7) % 9] += state[i % 9];
        }

        return state.Sum();
    }

    public object Part1()
    {
        return RunNIterations(80);
    }

    public object Part2()
    {
        return RunNIterations(256);
    }
}