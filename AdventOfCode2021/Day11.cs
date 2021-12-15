namespace AdventOfCode2021;

public class Octopus
{
    private int _energy;
    private Octopus[] _neighbors;
    public bool HasFlashed;

    public int FlashCount;

    public Octopus(char val)
    {
        _energy = int.Parse(val.ToString());
    }

    public void SetNeighbors(Octopus[] neighbors)
    {
        _neighbors = neighbors;
    }
    
    public void EndRound()
    {
        HasFlashed = false;
    }

    public void AddEnergy()
    {
        if (HasFlashed) return;
        
        _energy++;

        if (_energy > 9)
        {
            Flash();
        }
    }

    private void Flash()
    {
        HasFlashed = true;
        FlashCount++;
        
        foreach (var n in _neighbors)
        {
            n.AddEnergy();
        }

        _energy = 0;
    }
}

public class OctopusBoard
{
    private readonly Octopus[][] _board;

    public OctopusBoard(Octopus[][] board)
    {
        _board = board;

        for (var y = 0; y < board.Length; y++)
        {
            for (var x = 0; x < board[0].Length; x++)
            {
                _board[y][x].SetNeighbors(GetNeighbors(x, y));
            }
        }
    }

    private Octopus[] GetNeighbors(int x, int y)
    {
        var neighbors = new List<Octopus>();

        for (var ny = Math.Max(y - 1, 0); ny < Math.Min(y + 2, _board.Length); ny++)
        {
            for (var nx = Math.Max(x - 1, 0); nx < Math.Min(x + 2, _board[ny].Length); nx++)
            {
                if (nx == x && ny == y) continue;

                neighbors.Add(_board[ny][nx]);
            }
        }

        return neighbors.ToArray();
    }

    public int GetFlashCountAfterNSteps(int n)
    {
        for (var i = 0; i < n; i++)
        {
            foreach (var octopus in _board.SelectMany(a => a))
            {
                octopus.AddEnergy();
            }
            
            foreach (var octopus in _board.SelectMany(a => a))
            {
                octopus.EndRound();
            }
        }

        return GetFlashCount();
    }
    
    public int FindFirstSynchronousFlash()
    {
        var octopuses = _board.SelectMany(a => a);

        var step = 0;
        
        while(true)
        {
            step++;
            
            foreach (var octopus in octopuses)
            {
                octopus.AddEnergy();
            }

            if (octopuses.All(o => o.HasFlashed))
            {
                return step;
            }
            
            foreach (var octopus in octopuses)
            {
                octopus.EndRound();
            }
        }
    }

    private int GetFlashCount()
    {
        return _board.SelectMany(row => row.Select(o => o.FlashCount)).Sum();
    }
}

public class Day11 : Day, Parts
{
    private readonly Octopus[][] _grid;

    private OctopusBoard CreateOctopusBoard()
    {
        var grid = LinesStrings
            .Select((l, y) => l
                .ToCharArray()
                .Select((c,x) => new Octopus(c))
                .ToArray())
            .ToArray();

        return new OctopusBoard(grid);
    }

    public object Part1()
    {
        return CreateOctopusBoard().GetFlashCountAfterNSteps(100);
    }

    public object Part2()
    {
        return CreateOctopusBoard().FindFirstSynchronousFlash();
    }
}