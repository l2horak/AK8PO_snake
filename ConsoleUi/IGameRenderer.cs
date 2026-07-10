using SnakeClean.Core;

namespace SnakeClean.ConsoleUi;

public interface IGameRenderer
{
    void ConfigureWindow(int width, int height);
    void Render(GameState state, GameSettings settings);
    void RenderGameOver(GameState state, GameSettings settings);
}