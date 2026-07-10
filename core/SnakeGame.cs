namespace SnakeClean.Core;

public sealed class SnakeGame
{
    private readonly GameSettings _settings;
    private readonly Random _random;
    private readonly Snake _snake;

    public SnakeGame(GameSettings settings, Random random)
    {
        _settings = settings;
        _random = random;

        var start = new Position(settings.BoardWidth / 2, settings.BoardHeight / 2);
        _snake = new Snake(start, Direction.Right);
        Berry = GenerateBerryPosition();
        Score = settings.InitialSnakeLength;
    }

    public Position Berry { get; private set; }
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    public void ChangeDirection(Direction direction)
    {
        _snake.ChangeDirection(direction);
    }

    public void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        var nextHead = _snake.Head.Move(_snake.Direction);
        var berryEaten = nextHead == Berry;

        _snake.Move(berryEaten);

        if (berryEaten)
        {
            Score++;
            Berry = GenerateBerryPosition();
        }

        IsGameOver = HitsWall() || _snake.HitsItself();
    }

    public GameState GetState()
    {
        return new GameState(_snake.Body, _snake.Head, Berry, Score, IsGameOver);
    }

    private bool HitsWall()
    {
        return _snake.Head.X <= 0
            || _snake.Head.X >= _settings.BoardWidth - 1
            || _snake.Head.Y <= 0
            || _snake.Head.Y >= _settings.BoardHeight - 1;
    }

    private Position GenerateBerryPosition()
    {
        Position candidate;

        do
        {
            candidate = new Position(
                _random.Next(1, _settings.BoardWidth - 1),
                _random.Next(1, _settings.BoardHeight - 1));
        }
        while (candidate == _snake.Head || _snake.Body.Contains(candidate));

        return candidate;
    }
}