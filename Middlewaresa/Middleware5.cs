﻿using Serilog;

namespace JustTest.Middlewaresa
{
    public class Middleware5 : MiddlewareBase
    {
        public Middleware5()
        {
            id = 5;
        }
        public override async Task InvokeAsync(HttpContext context)
        {
            Log.Verbose($"Running Middleware {GetType().Name} ID: {id}");

            if (ShouldThrowException)
            {
                throw new Exception($"Middleware {GetType().Name} throw ex!");
            }
            else
            {
                Log.Verbose($"Middleware {GetType().Name} just return succeed response.");

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync($"{GetType().Name} ended success.");
                }
            }

            await _next(context);
        }
    }
}