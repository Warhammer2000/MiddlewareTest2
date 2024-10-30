// <copyright file="Program.cs" company="PlaceholderCompany">
// """
// </copyright>
namespace JustTest
{
    using JustTest.Middlewaresa;
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    /// <summary>
    /// Entry point for the JustTest application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
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