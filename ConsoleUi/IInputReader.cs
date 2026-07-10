using SnakeClean.Core;

namespace SnakeClean.ConsoleUi;

public interface IInputReader
{
    Direction? ReadDirection(TimeSpan timeout);
}