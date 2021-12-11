namespace AdventOfCode2021;

public class SymbolLine
{
    private readonly char[] _symbols;
    public long Score;
    public bool IsCorrupted;
    public bool IsIncomplete;
    private static readonly HashSet<char> StartingSymbols = new() { '[', '(', '<', '{' };

    private static readonly Dictionary<char, char> EndToStart = new()
    {
        { ']', '[' },
        { ')', '(' },
        { '>', '<' },
        { '}', '{' }
    };

    private static readonly Dictionary<char, char> StartToEnd = new()
    {
        { '[', ']' },
        { '(', ')' },
        { '<', '>' },
        { '{', '}' }
    };

    private static readonly Dictionary<char, int> CorruptedScoreTable = new()
    {
        { ']', 57 },
        { ')', 3 },
        { '>', 25137 },
        { '}', 1197 }
    };

    private static readonly Dictionary<char, int> MissingScoreTable = new()
    {
        { ']', 2 },
        { ')', 1 },
        { '>', 4 },
        { '}', 3 }
    };


    public SymbolLine(string line)
    {
        _symbols = line.ToCharArray();

        Simulate();
    }

    private static long GetMissingScore(IEnumerable<char> missing)
    {
        long score = 0;

        foreach (var symbol in missing)
        {
            score *= 5;
            score += MissingScoreTable[symbol];
        }

        return score;
    }

    private void Simulate()
    {
        var symbols = new Stack<char>();

        foreach (var symbol in _symbols)
        {
            if (StartingSymbols.Contains(symbol))
            {
                symbols.Push(symbol);
            }
            else
            {
                var startSymbol = EndToStart[symbol];

                if (startSymbol == symbols.Peek())
                {
                    symbols.Pop();
                }
                else
                {
                    IsCorrupted = true;
                    Score = CorruptedScoreTable[symbol];

                    return;
                }
            }
        }

        if (symbols.Count == 0) return;

        IsIncomplete = true;
        Score = GetMissingScore(symbols.Select(s => StartToEnd[s]));
    }
}

public class Day10 : Day
{
    private readonly IEnumerable<SymbolLine> _lines;

    public Day10()
    {
        _lines = LinesStrings.Select(l => new SymbolLine(l));
    }

    public long Part1()
    {
        return _lines
            .Where(l => l.IsCorrupted)
            .Select(l => l.Score)
            .Sum();
    }

    public long Part2()
    {
        var scores = _lines
            .Where(l => l.IsIncomplete)
            .Select(l => l.Score)
            .OrderBy(l => l)
            .ToArray();

        return scores[scores.Length / 2];
    }
}