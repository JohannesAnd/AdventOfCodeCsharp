using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AdventOfCode2021;

public class MathTreeJsonConverter : JsonConverter<MathTree>
{
    public override MathTree Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var trees = new Stack<MathTree>();
        trees.Push(new MathTree());

        do
        {
            var val = reader.TokenType;
            switch (val)
            {
                case JsonTokenType.StartArray:
                {
                    var newTree = new MathTree();
                    var top = trees.Peek();
                    top.Children = top.Children.Append(newTree).ToArray();
                    trees.Push(newTree);
                    break;
                }
                case JsonTokenType.EndArray:
                    trees.Pop();
                    break;
                case JsonTokenType.Number:
                {
                    var newTree = new MathTree { Value = reader.GetInt16() };
                    var top = trees.Peek();
                    top.Children = top.Children.Append(newTree).ToArray();
                    break;
                }
            }

            reader.Read();
        } while (trees.Count > 1);

        return trees.Pop().Children[0];
    }

    public override void Write(Utf8JsonWriter writer, MathTree value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

public class MathTree
{
    public int Value;
    public MathTree[] Children = Array.Empty<MathTree>();

    public long GetMagnitude()
    {
        if (Children.Length == 0) return Value;

        return Children[0].GetMagnitude() * 3 + Children[1].GetMagnitude() * 2;
    }
}

public class SnailFishMath
{
    private string _expression = string.Empty;

    public string GetExpression()
    {
        return _expression;
    }

    public long GetMagnitude()
    {
        var tree = JsonSerializer.Deserialize<MathTree>(_expression, new JsonSerializerOptions
        {
            Converters = { new MathTreeJsonConverter() }
        });

        return tree?.GetMagnitude() ?? -1;
    }

    public SnailFishMath Add(string value)
    {
        if (_expression.Length == 0)
        {
            _expression = value;
            EnsureReduced();

            return this;
        }

        _expression = $"[{_expression},{value}]";

        EnsureReduced();

        return this;
    }

    private void EnsureReduced()
    {
        var hasChanged = true;

        while (hasChanged)
        {
            hasChanged = false;

            if (Explode())
            {
                hasChanged = true;
                continue;
            }

            if (Split())
            {
                hasChanged = true;
            }
        }
    }

    private (int, int) GetExplodeIndex()
    {
        var stack = new Stack<char>();

        for (var i = 0; i < _expression.Length; i++)
        {
            if (_expression[i] == '[')
            {
                stack.Push('[');
            }

            if (_expression[i] == ']')
            {
                stack.Pop();
            }

            if (stack.Count <= 4) continue;

            var endIndex = _expression.IndexOf(']', i);

            return (i + 1, endIndex);
        }

        return (-1, -1);
    }

    private string AddBefore(int index, int value)
    {
        var beforeString = _expression[..index];
        var match = Regex.Match(beforeString, "[0-9]+", RegexOptions.RightToLeft);
        var last = match.Captures.LastOrDefault();

        if (last == null) return beforeString;

        var newValue = (int.Parse(last.Value) + value).ToString();

        return beforeString.Remove(last.Index, last.Value.Length).Insert(last.Index, newValue);
    }

    private string AddAfter(int index, int value)
    {
        var beforeString = _expression[(index + 1)..];
        var match = Regex.Match(beforeString, "[0-9]+");
        var first = match.Captures.FirstOrDefault();

        if (first == null) return beforeString;

        var newValue = (int.Parse(first.Value) + value).ToString();

        return beforeString.Remove(first.Index, first.Value.Length).Insert(first.Index, newValue);
    }

    private bool Explode()
    {
        var (startIndex, endIndex) = GetExplodeIndex();

        if (startIndex == -1) return false;

        var current = _expression[startIndex..endIndex].Split(',');

        var firstNumber = int.Parse(current[0]);
        var lastNumber = int.Parse(current[1]);

        var before = AddBefore(startIndex - 1, firstNumber);
        var after = AddAfter(endIndex, lastNumber);

        _expression = before + "0" + after;

        return true;
    }

    private bool Split()
    {
        var (startIndex, endIndex) = GetSplitIndex();

        if (startIndex == -1) return false;

        float value = int.Parse(_expression[startIndex..endIndex]);

        var firstValue = (int)Math.Floor(value / 2);
        var lastValue = (int)Math.Ceiling(value / 2);

        var newString = $"[{firstValue},{lastValue}]";

        _expression = _expression.Remove(startIndex, endIndex - startIndex).Insert(startIndex, newString);

        return true;
    }

    private (int, int) GetSplitIndex()
    {
        var numbers = _expression.Split(',', '[', ']').Where(s => s.Length > 0).Select(int.Parse).Where(n => n > 9)
            .ToArray();

        if (numbers.Length == 0) return (-1, -1);

        var number = numbers.First();

        for (var i = 0; i < _expression.Length; i++)
        {
            if (_expression.Substring(i, number.ToString().Length) == number.ToString())
            {
                return (i, i + number.ToString().Length);
            }
        }

        return (-1, -1);
    }
}

public class Day18 : Day, Parts
{
    public object Part1()
    {
        return LinesStrings.Aggregate(new SnailFishMath(), (m, val) => m.Add(val)).GetMagnitude();
    }

    public object Part2()
    {
       return LinesStrings
            .SelectMany(s1 => LinesStrings.Select(s2 => new { one = s1, two = s2 }))
            .Where(pair => !string.Equals(pair.one,pair.two))
            .Select(pair => new SnailFishMath().Add(pair.one).Add(pair.two).GetMagnitude())
            .Max();
    }
}