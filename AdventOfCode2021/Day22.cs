using System.Text.RegularExpressions;

namespace AdventOfCode2021;

public class Step
{
    public readonly bool Value;
    public readonly long FromX;
    public readonly long ToX;
    public readonly long FromY;
    public readonly long ToY;
    public readonly long FromZ;
    public readonly long ToZ;

    public Step(string input)
    {
        var match = Regex.Match(input,
            "(?<mode>on|off) x=(?<xFrom>-?[0-9]+)..(?<xTo>-?[0-9]+),y=(?<yFrom>-?[0-9]+)..(?<yTo>-?[0-9]+),z=(?<zFrom>-?[0-9]+)..(?<zTo>-?[0-9]+)$");

        Value = match.Groups["mode"].Value == "on";
        FromX = int.Parse(match.Groups["xFrom"].Value);
        ToX = int.Parse(match.Groups["xTo"].Value);
        FromY = int.Parse(match.Groups["yFrom"].Value);
        ToY = int.Parse(match.Groups["yTo"].Value);
        FromZ = int.Parse(match.Groups["zFrom"].Value);
        ToZ = int.Parse(match.Groups["zTo"].Value);
    }
}

public class Reboot
{
    private readonly Step[] _steps;
    private bool[][][] _status;
    private long[][][] _sizes;


    public Reboot(string[] steps)
    {
        _steps = steps.Select(step => new Step(step)).ToArray();
        var xValues = _steps.SelectMany(step => new[] { step.FromX, step.ToX + 1 }).ToHashSet().OrderBy(v => v)
            .ToArray();
        var yValues = _steps.SelectMany(step => new[] { step.FromY, step.ToY + 1 }).ToHashSet().OrderBy(v => v)
            .ToArray();
        var zValues = _steps.SelectMany(step => new[] { step.FromZ, step.ToZ + 1 }).ToHashSet().OrderBy(v => v)
            .ToArray();

        _status = Enumerable.Range(0, xValues.Length - 1)
            .Select(_ => Enumerable.Range(0, yValues.Length - 1)
                .Select(_ => Enumerable.Range(0, zValues.Length - 1)
                    .Select(_ => false)
                    .ToArray())
                .ToArray())
            .ToArray();

        _sizes = Enumerable.Range(0, xValues.Length - 1)
            .Select(x => Enumerable.Range(0, yValues.Length - 1)
                .Select(y => Enumerable.Range(0, zValues.Length - 1)
                    .Select(z => 
                        ((xValues[x + 1] - xValues[x]) * (yValues[y + 1] - yValues[y]) * (zValues[z + 1] - zValues[z])))
                    .ToArray())
                .ToArray())
            .ToArray();

        var s = 0;
        foreach (var step in _steps)
        {
            Console.WriteLine($"Step {s++} of {_steps.Length}");
            var xVal = xValues
                .Select((v, i) => new { v, i })
                .Where(v => step.FromX <= v.v && v.v <= step.ToX)
                .Select(v => v.i);
            var yVal = yValues
                .Select((v, i) => new { v, i })
                .Where(v => step.FromY <= v.v && v.v <= step.ToY)
                .Select(v => v.i);
            var zVal = zValues
                .Select((v, i) => new { v, i })
                .Where(v => step.FromZ <= v.v && v.v <= step.ToZ)
                .Select(v => v.i);

            foreach (var x in xVal)
            {
                foreach (var y in yVal)
                {
                    foreach (var z in zVal)
                    {
                        if (_status[x][y][z] != step.Value)
                        {
                            _status[x][y][z] = step.Value;
                        }
                    }
                }
            }
        }
    }

    public long GetOnCount()
    {
        return _status.SelectMany((xAxis, x) =>
            xAxis.SelectMany((yAxis, y) =>
                yAxis.Select((value, z) =>
                    value ? _sizes[x][y][z] : 0
                )
            )
        ).Sum();
    }
}

public class Day22 : Day, Parts
{
    public object Part1()
    {
        var reboot = new Reboot(LinesStrings[..20]);

        return reboot.GetOnCount();
    }

    public object Part2()
    {
        var reboot = new Reboot(LinesStrings);

        return reboot.GetOnCount();
    }
}