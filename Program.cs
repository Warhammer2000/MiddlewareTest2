using JustTest.Middlewaresa;
using JustTest.MiddlewareSettings;
using Microsoft.Extensions.FileProviders;
using Serilog;
namespace JustTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Trace()
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Services.AddSingleton<MiddlewareSelector>();

            Log.Verbose("Program started.");


            var app = builder.Build();

            app.UseMiddleware<Middleware4>();
            app.UseMiddleware<Middleware1>();
            app.UseMiddleware<Middleware2>();
            app.UseMiddleware<Middleware3>();
            app.UseMiddleware<Middleware5>();

            app.Run();
        }
    }
}