// See https://aka.ms/new-console-template for more information
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.ConsoleGame;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;
using Microsoft.Extensions.DependencyInjection;

Console.Title = "Interactive Fiction";

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddInteractiveFictionBusiness()
    .AddSingleton<IGameMenu, GameMenu>()
    .AddSingleton<IMenuStateFactory, MenuStateFactory>()
    .AddSingleton<ICommandParser, MenuCommandParser>()
    .AddSingleton<IConsoleGameRunner, ConsoleGameRunner>()
    .AddSingleton<IGameContainer, GameContainer>()
    .AddSingleton<IProcedureCommandParser, ProcedureCommandParser>()
    .AddSingleton<IArgParserFactory, ArgParserFactory>()
    .AddSingleton<ITextDecorator, ConsoleTextDecorator>()
    .AddSingleton<IObserverFactory, ObserverFactory>()
    .BuildServiceProvider();

var gameRunner = serviceProvider.GetService<IConsoleGameRunner>();
if (gameRunner == null)
{
    throw new Exception("Unable to start game with null runner.");
}

while (true)
{
    Console.WriteLine(gameRunner.GetScreen());
    Console.Write(">> ");
    var command = Console.ReadLine();
    Console.Clear();
    if (!string.IsNullOrEmpty(command))
    {
        gameRunner.Perform(command);
        gameRunner.Tick();
    }
}