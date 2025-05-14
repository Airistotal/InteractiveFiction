// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var builder = Host.CreateDefaultBuilder(args);

//builder.ConfigureServices(
//    services =>
//        services.AddHostedService<Worker>()
//            .AddScoped<IMessageWriter, MessageWriter>());

using var host = builder.Build();

host.Start();