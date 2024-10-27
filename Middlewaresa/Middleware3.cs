namespace JustTest.Middlewaresa
{
    public class Middleware3 : MiddlewareBase
    {
        public Middleware3()
        {
            id = 3;
        }
        public Middleware3(RequestDelegate next) : base(next)
        {
            HasTryCatch = false;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Выполняется Middleware3");
            await _next(context);
        }
    }
}
