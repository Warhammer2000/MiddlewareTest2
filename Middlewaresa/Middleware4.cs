namespace JustTest.Middlewaresa
{
    public class Middleware4 : MiddlewareBase
    {
        public Middleware4()
        {
            id = 4;
        }
        public Middleware4(RequestDelegate next) : base(next)
        {
            HasTryCatch = false;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Выполняется Middleware4");
            await _next(context);
        }
    }
}
