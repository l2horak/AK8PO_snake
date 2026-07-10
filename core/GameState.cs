namespace SnakeClean.Core;

public sealed class GameState
{
    public GameState(IReadOnlyCollection<Position> body, Position head, Position berry, int score, bool isGameOver)
    {
        Body = body;
        Head = head;
        Berry = berry;
        Score = score;
        IsGameOver = isGameOver;
    }

    public IReadOnlyCollection<Position> Body { get; }
    public Position Head { get; }
    public Position Berry { get; }
    public int Score { get; }
    public bool IsGameOver { get; }
}