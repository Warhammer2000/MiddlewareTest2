namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    public class Middleware4 : MiddlewareBase
    {
        public Middleware4(RequestDelegate next, MiddlewareSelector selector) : base(next, selector, 4) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await base.InvokeAsync(context);
            }
            catch (Exception ex)
            {
                Log.Error($"Exception in {GetType().Name}: {ex.Message}");

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/plain";

                    LogBufferHelper.AddLog(context, $"Exception in {GetType().Name}: {ex.Message}");
                    
                    await context.Response.WriteAsync(LogBufferHelper.GetLogBuffer(context));
                }
            }
        }
    }
}
