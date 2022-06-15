// See https://aka.ms/new-console-template for more information
using Serilog;

Console.WriteLine("Hello, World!");


Log.Logger = new LoggerConfiguration()
                  .WriteTo.Seq("http://20.23.6.44")
                  .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Month)
                  .Enrich.WithMachineName()
                  .Enrich.WithEnvironmentUserName()
                  //.Enrich.WithExceptionDetails()
                  .CreateLogger();

Log.Warning("HILFEE!!!");