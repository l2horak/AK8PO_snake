using SnakeClean.Application;
using SnakeClean.ConsoleUi;
using SnakeClean.Core;

var settings = new GameSettings(
    boardWidth: 32,
    boardHeight: 16,
    tickDuration: TimeSpan.FromMilliseconds(500),
    initialSnakeLength: 5);

var random = new Random();
var renderer = new ConsoleRenderer();
var inputReader = new ConsoleInputReader();
var gameFactory = new SnakeGameFactory(random);
var application = new SnakeConsoleApplication(settings, renderer, inputReader, gameFactory);

application.Run();
