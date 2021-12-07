namespace AdventOfCode2021;

public class Day7 : Day
{
    private readonly int[] _numbers;
    private readonly int _median;
    private readonly int _average;

    public Day7()
    {
        _numbers = LinesStrings[0]
            .Split(',')
            .Select(int.Parse)
            .OrderBy(val => val)
            .ToArray();
        _median = _numbers[_numbers.Length / 2];
        _average = _numbers.Sum() / _numbers.Length;
    }

    private int GetQuadraticCost(int number)
    {
        return _numbers
            .Select(n => Math.Abs(n - number))
            .SelectMany(n => Enumerable.Range(1, n))
            .Sum();
    }
    
    private int GetLinearCost(int number)
    {
        return _numbers
            .Select(n => Math.Abs(n - number))
            .Sum();
    }

    public long Part1()
    {
        return GetLinearCost(_median);
    }

    public long Part2()
    {
        return  GetQuadraticCost(_average);
    }
}