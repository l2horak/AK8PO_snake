using SnakeClean.Core;

namespace SnakeClean.Application;

public sealed class SnakeGameFactory
{
    private readonly Random _random;

    public SnakeGameFactory(Random random)
    {
        _random = random;
    }

    public SnakeGame Create(GameSettings settings)
    {
        return new SnakeGame(settings, _random);
    }
}