using SnakeClean.ConsoleUi;
using SnakeClean.Core;

namespace SnakeClean.Application;

public sealed class SnakeConsoleApplication
{
    private readonly GameSettings _settings;
    private readonly IGameRenderer _renderer;
    private readonly IInputReader _inputReader;
    private readonly SnakeGameFactory _gameFactory;

    public SnakeConsoleApplication(
        GameSettings settings,
        IGameRenderer renderer,
        IInputReader inputReader,
        SnakeGameFactory gameFactory)
    {
        _settings = settings;
        _renderer = renderer;
        _inputReader = inputReader;
        _gameFactory = gameFactory;
    }

    public void Run()
    {
        _renderer.ConfigureWindow(_settings.BoardWidth, _settings.BoardHeight);

        var game = _gameFactory.Create(_settings);

        while (!game.GetState().IsGameOver)
        {
            _renderer.Render(game.GetState(), _settings);

            var direction = _inputReader.ReadDirection(_settings.TickDuration);
            if (direction.HasValue)
            {
                game.ChangeDirection(direction.Value);
            }

            game.Update();
        }

        _renderer.RenderGameOver(game.GetState(), _settings);
    }
}