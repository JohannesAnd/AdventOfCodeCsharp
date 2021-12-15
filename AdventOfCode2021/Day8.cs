namespace AdventOfCode2021;

public class Display
{
    private readonly string[] _data;
    private readonly string[] _numbers;
    private readonly List<string> _newNumbers;

    public Display(string input)
    {
        var split = input.IndexOf('|');

        _numbers = input[.. split].Trim().Split(' ');
        _data = input[(split + 1) ..].Trim().Split(' ');

        _newNumbers = Solve();
    }

    private List<string> Solve()
    {
        var one = _numbers.First(n => n.Length == 2).ToCharArray();
        var four = _numbers.First(n => n.Length == 4).ToCharArray();
        var seven = _numbers.First(n => n.Length == 3).ToCharArray();
        var eight = _numbers.First(n => n.Length == 7).ToCharArray();


        // Out of the numbers with 6 segments, only nine has all the characters of four
        var nine = _numbers.First(l => l.Length == 6 && four.All(l.Contains)).ToCharArray();

        // The letter missing in nine is e
        var e = eight.First(l => !nine.Contains(l));

        // Only two has e out of the ones with length 5
        var two = _numbers.First(l => l.Length == 5 && l.Contains(e)).ToCharArray();

        // In one but not two
        var f = one.First(l => !two.Contains(l));

        // The one not f in one is c
        var c = one.First(l => l != f);

        // Five is the only of length 5 with no c
        var five = _numbers.First(l => l.Length == 5 && !l.Contains(c)).ToCharArray();

        // Three is the last of length 5
        var three = _numbers.First(l => l.Length == 5 && !five.All(l.Contains) && !two.All(l.Contains)).ToCharArray();

        // Six is only of length 6 missing c
        var six = _numbers.First(l => l.Length == 6 && !l.Contains(c)).ToCharArray();

        // Zero is the last of length 6
        var zero = _numbers.First(l => l.Length == 6 && !six.All(l.Contains) && !nine.All(l.Contains)).ToCharArray();

        return new[] { zero, one, two, three, four, five, six, seven, eight, nine }
            .Select(d => OrderString(new String(d))).ToList();
    }

    private static string OrderString(string input)
    {
        return new String(input.ToCharArray().OrderBy(l => l).ToArray());
    }

    public int GetValue()
    {
        return int.Parse(string.Join("",
            _data.Select(number => _newNumbers.IndexOf(OrderString(number)).ToString()).ToArray()));
    }

    public int GetCountOf1478()
    {
        return _data.Count(d => new[] { 2, 3, 4, 7 }.Contains(d.Length));
    }
}

public class Day8 : Day, Parts
{
    private readonly Display[] _displays;

    public Day8()
    {
        _displays = LinesStrings
            .Select(val => new Display(val))
            .ToArray();
    }

    public object Part1()
    {
        return _displays.Select(d => d.GetCountOf1478()).Sum();
    }

    public object Part2()
    {
        return _displays.Select(d => d.GetValue()).Sum();
    }
}