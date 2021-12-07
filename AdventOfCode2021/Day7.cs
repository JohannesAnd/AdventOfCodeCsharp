namespace AdventOfCode2021;

public class Day7 : Day
{
    private readonly int[] _numbers;
    private readonly int _median;

    public Day7()
    {
        _numbers = LinesStrings[0]
            .Split(',')
            .Select(int.Parse)
            .OrderBy(val => val)
            .ToArray();
        _median = _numbers[_numbers.Length / 2];
    }

    private int GetCost(int number)
    {
        return _numbers
            .Select(n => Math.Abs(n - number))
            .SelectMany(n => Enumerable.Range(1, n))
            .Sum();
    }

    public long Part1()
    {
        return _numbers.Select(number => Math.Abs(number - _median)).Sum();
    }

    public long Part2()
    {
        var position = _median;
        var value = GetCost(position);
        var valueBefore = GetCost(position + 1);

        var direction = Math.Sign(value - valueBefore);

        while (Math.Sign(value - valueBefore) == direction)
        {
            position += direction;
            value = GetCost(position);
            valueBefore = GetCost(position + 1);
        }

        return Math.Min(value, valueBefore);
    }
}