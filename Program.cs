using JustTest.Middlewaresa;
using Microsoft.Extensions.FileProviders;
using Serilog;
namespace JustTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSingleton<RandomMiddlewareExecutor>();
            
            builder.Services.AddTransient<SelectedMiddlewareExecutor>();

            builder.Services.AddTransient<Middleware1>();
            builder.Services.AddTransient<Middleware2>();
            builder.Services.AddTransient<Middleware3>();
            builder.Services.AddTransient<Middleware4>();
            builder.Services.AddTransient<Middleware5>();
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.Trace()
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Verbose("Program started.");

            var app = builder.Build();

            var randomExecutor = app.Services.GetRequiredService<RandomMiddlewareExecutor>();

            var selectedMiddleware = randomExecutor.GetSelectedMiddleware();

            app.UseSelectedMiddlewareExecutor(selectedMiddleware);

            app.Run();
        }
    }
}