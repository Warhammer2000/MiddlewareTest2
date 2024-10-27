namespace JustTest.Middlewaresa
{
    public class ErrorHandlingMiddleware : MiddlewareBase
    {
        public ErrorHandlingMiddleware()
        {
            id = 1;
        }
        public ErrorHandlingMiddleware(RequestDelegate next) : base(next)
        {
            HasTryCatch  =true;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"An error has occurred {GetType().Name}: {ex.Message}");
                // Возвращаем пользовательский ответ с информацией об ошибке
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An internal server error has occurred.");
            }
        }
    }
}