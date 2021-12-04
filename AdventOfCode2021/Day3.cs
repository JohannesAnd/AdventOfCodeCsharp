namespace AdventOfCode2021;

public class Day3 : Day
{
    private readonly int[][] _bits;

    public Day3()
    {
        _bits = LinesStrings
            .Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
    }

    private int[] GetCount(IReadOnlyCollection<int[]> input, int larger, int smaller)
    {
        return input.Aggregate(GetEmpty(), (line1, line2) => line1.Zip(line2, (a, b) => a + b).ToArray())
            .Select(num =>
            {
                if (num * 2 == input.Count) return -1;

                return num > input.Count / 2 ? larger : smaller;
            })
            .ToArray();
    }

    private static int ArrayToInt(IEnumerable<int> strings)
    {
        return Convert.ToInt16(string.Join("", strings), 2);
    }

    private IEnumerable<int> GetEmpty()
    {
        return Enumerable.Range(0, LinesStrings[0].Length).Select(_ => 0);
    }

    private int Find(int larger, int smaller, int tieBreak)
    {
        var result = _bits.ToArray();

        for (var i = 0; i < _bits[0].Length && result.Length > 1; i++)
        {
            var count = GetCount(result, larger, smaller);
            var isEven = count[i] == -1;

            result = result
                .Where(line => isEven && line[i] == tieBreak || !isEven && count[i] == line[i])
                .ToArray();
        }

        return ArrayToInt(result[0]);
    }

    public int Part1()
    {
        var gammaRate = ArrayToInt(GetCount(_bits, 1, 0));
        var epsilonRate = ArrayToInt(GetCount(_bits, 0, 1));

        return gammaRate * epsilonRate;
    }


    public int Part2()
    {
        var oxygen = Find(1, 0, 1);
        var co2 = Find(0, 1, 0);

        return oxygen * co2;
    }
}