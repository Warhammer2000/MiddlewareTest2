using Serilog;

namespace JustTest.Middlewaresa
{
    public class MiddlewareBase
    {
        public int id;
        public RequestDelegate _next;
        public bool ShouldThrowException = false;

        public MiddlewareBase() { }
        public void SetNext(RequestDelegate next)
        {
            _next = next;
        }
        public virtual async Task InvokeAsync(HttpContext context)
        {
            Log.Verbose($"Running Middleware {GetType().Name} ID: {id}");

            if (ShouldThrowException)
            {
                throw new Exception($"Middleware {GetType().Name}  thrown ex!");
            }
            else
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync($"{GetType().Name} ended successfuly.");
                }
            }

            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}
