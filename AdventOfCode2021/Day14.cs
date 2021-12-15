namespace AdventOfCode2021;

class Polymer
{
    private readonly char[] _polymer;
    private readonly Dictionary<string, char[]> _rules = new();
    private readonly Dictionary<string, Dictionary<char, long>> _cache = new();


    public Polymer(string[] input)
    {
        _polymer = input[0].ToCharArray();

        foreach (var s in input[2..])
        {
            var split = s.Split(' ');
            var chars = split[0].ToCharArray();

            _rules.Add(new string(split[0]), $"{chars[0]}{split[2]}{chars[1]}".ToCharArray());
        }
    }

    private static Dictionary<char, long> MergeDictionaries(IEnumerable<Dictionary<char, long>> toMerge)
    {
        var result = new Dictionary<char, long>();

        foreach (var dict in toMerge)
        {
            foreach (var (key, value) in dict)
            {
                if (result.ContainsKey(key))
                {
                    result[key] += value;
                }
                else
                {
                    result.Add(key, value);
                }
            }
        }

        return result;
    }

    public Dictionary<char, long> Grow(int n)
    {
        var expanded = _polymer
            .Window(2)
            .Select(pair =>
            {
                var expansion = GrowRecursive(pair, n);

                return new
                {
                    pair,
                    expansion
                };
            })
            .ToArray();

        var result = new Dictionary<char, long>();

        foreach (var exp in expanded)
        {
            foreach (var (key, value) in exp.expansion)
            {
                if (result.ContainsKey(key))
                {
                    result[key] += value;
                }
                else
                {
                    result.Add(key, value);
                }
            }
        }

        // Add missing last character
        if (result.ContainsKey(_polymer.Last()))
        {
            result[_polymer.Last()] += 1;
        }
        else
        {
            result.Add(_polymer.Last(), 1);
        }

        return result;
    }

    private Dictionary<char, long> GrowRecursive(IList<char> array, int n)
    {
        var input = new string(array.ToArray());

        if (n == 0)
        {
            return new Dictionary<char, long> { { array[0], 1 } };
        }

        var cacheKey = $"{n}{input}";

        if (_cache.ContainsKey(cacheKey))
        {
            return _cache[cacheKey];
        }

        var newPolymer = _rules[input];

        var expanded = newPolymer
            .Window(2)
            .Select(pair => new
                {
                    pair,
                    expansion = GrowRecursive(pair, n - 1)
                }
            )
            .ToArray();

        var result = new Dictionary<char, long>();

        foreach (var exp in expanded)
        {
            foreach (var (key, value) in exp.expansion)
            {
                if (result.ContainsKey(key))
                {
                    result[key] += value;
                }
                else
                {
                    result.Add(key, value);
                }
            }
        }

        if (!_cache.ContainsKey(cacheKey))
        {
            _cache.Add(cacheKey, result);
        }

        return result;
    }
}

public class Day14 : Day
{
    private long ComputeValue(int n)
    {
        var values = new Polymer(LinesStrings)
            .Grow(n)
            .Values
            .OrderBy(v => v)
            .ToList();

        return values.Last() - values.First();
    }

    public long Part1()
    {
        return ComputeValue(10);
    }

    public long Part2()
    {
        return ComputeValue(40);
    }
}