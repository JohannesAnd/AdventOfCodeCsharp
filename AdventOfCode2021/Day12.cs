namespace AdventOfCode2021;

public class Cave
{
    public readonly bool IsLarge;
    public readonly bool IsStart;
    public readonly bool IsEnd;

    public readonly List<Cave> Neighbors = new();

    public Cave(string name)
    {
        IsLarge = name.ToUpper() == name;
        IsStart = name == "start";
        IsEnd = name == "end";
    }

    public void AddNeighbor(Cave c)
    {
        if (!Neighbors.Contains(c))
        {
            Neighbors.Add(c);
        }
    }
}

public class Day12 : Day, Parts
{
    private Cave CreateCaveSystem()
    {
        var caves = new Dictionary<string, Cave>();

        foreach (var input in LinesStrings)
        {
            var parts = input.Split('-');

            if (!caves.ContainsKey(parts[0]))
            {
                caves.Add(parts[0], new Cave(parts[0]));
            }

            if (!caves.ContainsKey(parts[1]))
            {
                caves.Add(parts[1], new Cave(parts[1]));
            }

            caves[parts[0]].AddNeighbor(caves[parts[1]]);
            caves[parts[1]].AddNeighbor(caves[parts[0]]);
        }

        return caves["start"];
    }

    private static Cave[][] GetPaths(Cave c, Cave[] maybeCaves = null)
    {
        var caves = maybeCaves ?? Array.Empty<Cave>();

        if (c.IsEnd) return new[] { caves.Append(c).ToArray() };

        return c.Neighbors
            .Where(n => n.IsLarge || !caves.Contains(n))
            .SelectMany(n => GetPaths(n, caves.Append(c).ToArray()).Where(p => p.Length > 0))
            .ToArray();
    }

    private static Cave[][] GetPathsWithDoubleSmall(Cave c, Cave[] maybeCaves = null, bool hasDoneDouble = false)
    {
        var caves = maybeCaves ?? Array.Empty<Cave>();
        
        if (c.IsEnd) return new[] { caves.Append(c).ToArray() };

        return c.Neighbors
            .Where(n => !n.IsStart && (n.IsLarge || !hasDoneDouble || !caves.Contains(n)))
            .SelectMany(n =>
                GetPathsWithDoubleSmall(n, caves.Append(c).ToArray(), hasDoneDouble || caves.Contains(n) && !n.IsLarge)
                    .Where(p => p.Length > 0))
            .ToArray();
    }

    public object Part1()
    {
        return GetPaths(CreateCaveSystem()).Length;
    }

    public object Part2()
    {
        return GetPathsWithDoubleSmall(CreateCaveSystem()).Length;
    }
}