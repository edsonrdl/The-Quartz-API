using Serilog;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartzService();

var logConfiguration = new LoggerConfiguration()//Para exibir o console no terminal 
    .WriteTo.Debug()
    .WriteTo.Console();
    //.WriteTo.File("Logs/log.txt"); //para registrar o console no log.txt

Log.Logger = logConfiguration.CreateLogger();

var app = builder.Build();

await app.AddQuartzSchedules();

Log.Information("Starting api");
app.Run();