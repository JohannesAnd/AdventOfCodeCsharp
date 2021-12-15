namespace AdventOfCode2021;

public class Node
{
    public readonly int X;
    public readonly int Y;
    public readonly int Value;
    public bool IsLast;

    public Node(int value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;
    }

    public void SetIsLast()
    {
        IsLast = true;
    }
}

public class Day15 : Day, Parts
{
    private readonly Node[][] _map;
    private readonly Node[][] _bigMap;

    public Day15()
    {
        _map = LinesStrings
            .Select((row, y) => row
                .ToCharArray()
                .Select((val, x) => new Node(int.Parse(val.ToString()), x, y))
                .ToArray())
            .ToArray();

        _map.Last().Last().SetIsLast();

        _bigMap = Enumerable.Range(0, 5 * LinesStrings.Length).Select(yIndex => Enumerable
            .Range(0, 5 * LinesStrings[0].Length)
            .Select(xIndex =>
                new Node(
                    (xIndex / LinesStrings[0].Length + yIndex / LinesStrings.Length + GetValue(xIndex, yIndex) - 1) %
                    9 + 1,
                    xIndex,
                    yIndex)
            ).ToArray()
        ).ToArray();

        _bigMap.Last().Last().SetIsLast();
    }

    private int GetValue(int x, int y)
    {
        var x1 = x % LinesStrings[0].Length;
        var y1 = y % LinesStrings.Length;

        return int.Parse(LinesStrings[y1].Substring(x1, 1));
    }

    private static Node GetNode(Node[][] map, int x, int y)
    {
        if (y < 0 || y >= map.Length || x < 0 || x >= map[0].Length) return null;

        return map[y][x];
    }

    private static Node[] GetNeighbors(Node[][] map, int x, int y)
    {
        return new[]
            {
                GetNode(map, x, y + 1),
                GetNode(map, x + 1, y),
                GetNode(map, x, y - 1),
                GetNode(map, x - 1, y),
            }
            .Where(n => n != null)
            .ToArray();
    }

    private static int GetShortestPath(Node[][] map)
    {
        var visited = new HashSet<Node>();
        var newlyChanged = new HashSet<Node>();
        var unvisited = new HashSet<Node>(map.SelectMany(n => n));
        var distances = map
            .Select(row => row
                .Select(_ => int.MaxValue)
                .ToArray())
            .ToArray();

        distances[0][0] = 0;

        do
        {
            var node = (newlyChanged.Count > 0 ? newlyChanged : unvisited).OrderBy(n => distances[n.Y][n.X])
                .First();
            var neighbors = GetNeighbors(map, node.X, node.Y)
                .Where(n => !visited.Contains(n));

            foreach (var neighbor in neighbors)
            {
                var newDistance = distances[node.Y][node.X] + neighbor.Value;
                var currentDistance = distances[neighbor.Y][neighbor.X];

                if (newDistance < currentDistance)
                {
                    distances[neighbor.Y][neighbor.X] = newDistance;

                    if (!newlyChanged.Contains(neighbor)) newlyChanged.Add(neighbor);
                }
            }

            visited.Add(node);
            unvisited.Remove(node);
            if (newlyChanged.Contains(node)) newlyChanged.Remove(node);

            if (node.IsLast) return distances[node.Y][node.X];
        } while (unvisited.Count > 0);

        return -1;
    }

    public object Part1()
    {
        return GetShortestPath(_map);
    }

    public object Part2()
    {
        return GetShortestPath(_bigMap);
    }
}