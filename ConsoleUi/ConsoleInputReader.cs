using SnakeClean.Core;

namespace SnakeClean.ConsoleUi;

public sealed class ConsoleInputReader : IInputReader
{
    public Direction? ReadDirection(TimeSpan timeout)
    {
        var start = DateTime.UtcNow;
        var directionChanged = false;
        Direction? selectedDirection = null;

        while (DateTime.UtcNow - start < timeout)
        {
            if (!Console.KeyAvailable)
            {
                continue;
            }

            var key = Console.ReadKey(intercept: true).Key;
            if (directionChanged)
            {
                continue;
            }

            selectedDirection = MapKeyToDirection(key);
            directionChanged = selectedDirection.HasValue;
        }

        return selectedDirection;
    }

    private static Direction? MapKeyToDirection(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => null
        };
    }
}