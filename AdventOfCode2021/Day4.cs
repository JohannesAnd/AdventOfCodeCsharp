namespace AdventOfCode2021;

public struct Coordinates
{
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
}

public class Board
{
    private readonly List<List<int>> _board;
    private readonly List<List<bool>> _crossedOf;
    private readonly List<int> _crossedNumbers = new();
    public int NumberOfGuesses;
    private bool _hasWon;

    public Board(string[] value)
    {
        _board = value
            .Select(row => row.Split(' ')
                .Where(val => val != "")
                .Select(int.Parse)
                .ToList())
            .ToList();

        _crossedOf = value
            .Select(row => row.Split(' ')
                .Where(val => val != "")
                .Select(_ => false)
                .ToList())
            .ToList();
    }

    private Coordinates? FindNumber(int number)
    {
        for (var y = 0; y < _board.Count; y++)
        {
            for (var x = 0; x < _board[y].Count; x++)
            {
                if (_board[y][x] == number) return new Coordinates(x, y);
            }
        }

        return null;
    }

    private void CheckIfWon()
    {
        _hasWon = _crossedOf
            .Select((_, index) => _crossedOf
                .Select(row => row[index])
                .ToList())
            .Concat(_crossedOf)
            .Any(column => column
                .All(val => val));
    }

    public void CrossOfNumber(int number)
    {
        if (_hasWon) return;

        NumberOfGuesses++;

        var coordinate = FindNumber(number);

        if (!coordinate.HasValue) return;

        _crossedOf[coordinate.Value.Y][coordinate.Value.X] = true;
        _crossedNumbers.Add(number);

        if (_crossedNumbers.Count >= 5)
        {
            CheckIfWon();
        }
    }

    public int GetScore()
    {
        var sumOfUnmarkedNumbers = _board
            .SelectMany((row, y) => row
                .Where((_, x) => !_crossedOf[y][x]))
            .Sum();

        return sumOfUnmarkedNumbers * _crossedNumbers.Last();
    }
}

public class Day4 : Day, Parts
{
    private readonly List<Board> _boards;
    
    public Day4()
    {
        var instructions = LinesStrings[0].Split(',').Select(int.Parse).ToList();
        _boards = Enumerable
            .Range(0, (LinesStrings.Length - 1) / 6)
            .Select(index => new Board(
                LinesStrings
                    .Skip(index * 6 + 2)
                    .Take(5)
                    .ToArray()))
            .ToList();

        foreach (var number in instructions)
        {
            foreach (var board in _boards)
            {
                board.CrossOfNumber(number);
            }
        }

        _boards = _boards.OrderBy(board => board.NumberOfGuesses).ToList();
    }

    public object Part1()
    {
        return _boards.First().GetScore();
    }

    public object Part2()
    {
        return _boards.Last().GetScore();
    }
}