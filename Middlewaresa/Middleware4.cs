// <copyright file="Middleware4.cs" company="PlaceholderCompany">
// """
// </copyright>

namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Middleware with ID 4 that handles exceptions.
    /// </summary>
    public class Middleware4 : MiddlewareBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Middleware4"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the request pipeline.</param>
        /// <param name="selector">The middleware selector instance.</param>
        public Middleware4(RequestDelegate next, MiddlewareSelector selector)
            : base(next, selector, 4)
        {
        }

        /// <summary>
        /// Executes the middleware operation asynchronously, handling exceptions if they occur.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await base.InvokeAsync(context);
            }
            catch (Exception ex)
            {
                Log.Error($"Exception in {this.GetType().Name}: {ex.Message}");

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/plain";

                    LogBufferHelper.AddLog(context, $"Exception in {this.GetType().Name}: {ex.Message}");
                    await context.Response.WriteAsync(LogBufferHelper.GetLogBuffer(context));
                }

                return;
            }

            if (this.Next != null)
            {
                await this.Next(context);
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(LogBufferHelper.GetLogBuffer(context));
            }
        }
    }
}