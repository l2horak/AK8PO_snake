using SnakeClean.Core;

namespace SnakeClean.ConsoleUi;

public sealed class ConsoleRenderer : IGameRenderer
{
    private const string Pixel = "■";

    public void ConfigureWindow(int width, int height)
    {
        Console.CursorVisible = false;
        Console.WindowWidth = width;
        Console.WindowHeight = height;
    }

    public void Render(GameState state, GameSettings settings)
    {
        Console.Clear();
        DrawBorders(settings.BoardWidth, settings.BoardHeight);
        DrawBody(state.Body);
        DrawBerry(state.Berry);
        DrawHead(state.Head);
    }

    public void RenderGameOver(GameState state, GameSettings settings)
    {
        Console.Clear();
        Console.SetCursorPosition(settings.BoardWidth / 5, settings.BoardHeight / 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Game over, Score: {state.Score}");
    }

    private static void DrawBorders(int width, int height)
    {
        Console.ForegroundColor = ConsoleColor.White;

        for (var x = 0; x < width; x++)
        {
            DrawPixel(new Position(x, 0));
            DrawPixel(new Position(x, height - 1));
        }

        for (var y = 0; y < height; y++)
        {
            DrawPixel(new Position(0, y));
            DrawPixel(new Position(width - 1, y));
        }
    }

    private static void DrawBody(IEnumerable<Position> body)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        foreach (var segment in body)
        {
            DrawPixel(segment);
        }
    }

    private static void DrawHead(Position head)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(head);
    }

    private static void DrawBerry(Position berry)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        DrawPixel(berry);
    }

    private static void DrawPixel(Position position)
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.Write(Pixel);
    }
}