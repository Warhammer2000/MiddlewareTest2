using Serilog;

namespace JustTest.Middlewaresa
{
    public class SelectedMiddlewareExecutor
    {
        private readonly RequestDelegate _next;
        private readonly MiddlewareBase _selectedMiddleware;

        public SelectedMiddlewareExecutor(RequestDelegate next, MiddlewareBase selectedMiddleware)
        {
            _next = next;
            _selectedMiddleware = selectedMiddleware;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _selectedMiddleware.SetNext(_next);
                await _selectedMiddleware.InvokeAsync(context);
            }
            catch (Exception ex)
            {
                Log.Error($"An error has occurred: {ex.Message}");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"error occured {ex.Message}.");
            }
        }
    }
}
