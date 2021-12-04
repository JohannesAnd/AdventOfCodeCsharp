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

    private int[] GetCount(int[][] input, int larger, int smaller)
    {
        return input.Aggregate(GetEmpty(), (line1, line2) => line1.Zip(line2, (a, b) => a + b).ToArray())
            .Select(num =>
            {
                if (num * 2 == input.Length) return -1;

                return num > input.Length / 2 ? larger : smaller;
            })
            .ToArray();
    }

    private static int ArrayToInt(int[] strings)
    {
        return Convert.ToInt16(string.Join("", strings), 2);
    }

    private IEnumerable<int> GetEmpty()
    {
        return Enumerable.Range(0, LinesStrings[0].Length).Select(_ => 0);
    }

    public int Part1()
    {
        var gammaRate = ArrayToInt(GetCount(_bits, 1, 0));
        var epsilonRate = ArrayToInt(GetCount(_bits, 0, 1));

        return gammaRate * epsilonRate;
    }

    private int[] FindOxygen()
    {
        var result = _bits.ToArray();

        for (var i = 0; i < _bits[0].Length && result.Length > 1; i++)
        {
            var mostCommon = GetCount(result, 1, 0);

            var isEven = mostCommon[i] == -1;

            result = result
                .Where(line => isEven && line[i] == 1 || !isEven && mostCommon[i] == line[i])
                .ToArray();
        }

        return result[0];
    }

    private int[] FindCo2()
    {
        var result = _bits.ToArray();

        for (var i = 0; i < _bits[0].Length && result.Length > 1; i++)
        {
            var leastCommon = GetCount(result, 0, 1);

            var isEven = leastCommon[i] == -1;

            result = result
                .Where(line => isEven && line[i] == 0 || !isEven && leastCommon[i] == line[i])
                .ToArray();
        }

        return result[0];
    }

    public int Part2()
    {
        var oxygen = ArrayToInt(FindOxygen());
        var co2 = ArrayToInt(FindCo2());

        return oxygen * co2;
    }
}