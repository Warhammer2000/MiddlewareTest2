namespace JustTest.Middlewaresa
{
    using global::JustTest.MiddlewareSettings;
    using JustTest.Exceptions;
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    public class MiddlewareBase
    {
        protected readonly RequestDelegate _next;
        private readonly MiddlewareSelector _selector;
        public bool ShouldThrowException { get; set; } = false;
        public int Id { get; }

        public MiddlewareBase(RequestDelegate next, MiddlewareSelector selector, int id)
        {
            _next = next;
            _selector = selector;
            this.Id = id;
        }
        public virtual async Task InvokeAsync(HttpContext context)
        {
            LogBufferHelper.AddLog(context, $"[Middleware {Id}] Passing through {GetType().Name}");

            var selectedType = MiddlewareSelector.SelectedMiddlewareType;
            var shouldThrowException = MiddlewareSelector.ShouldThrowException;

            if (GetType() == selectedType)
            {
                LogBufferHelper.AddLog(context, $"[Middleware {Id}] Executing selected {GetType().Name}");

                if (shouldThrowException)
                {
                    LogBufferHelper.AddLog(context, $"[Middleware {Id}] {GetType().Name} generated an exception!");
                    throw new MiddlewareException(Id, $"{GetType().Name} generated an exception!");
                }

                LogBufferHelper.AddLog(context, $"[Middleware {Id}] {GetType().Name} completed successfully.");
            }

            await _next(context);

            LogBufferHelper.AddLog(context, $"[Middleware {Id}] Exiting {GetType().Name}");
            
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(LogBufferHelper.GetLogBuffer(context));
            }
        }
    }
}