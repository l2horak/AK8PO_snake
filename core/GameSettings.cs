namespace SnakeClean.Core;

public sealed class GameSettings
{
    public GameSettings(int boardWidth, int boardHeight, TimeSpan tickDuration, int initialSnakeLength)
    {
        BoardWidth = boardWidth;
        BoardHeight = boardHeight;
        TickDuration = tickDuration;
        InitialSnakeLength = initialSnakeLength;
    }

    public int BoardWidth { get; }
    public int BoardHeight { get; }
    public TimeSpan TickDuration { get; }
    public int InitialSnakeLength { get; }
}