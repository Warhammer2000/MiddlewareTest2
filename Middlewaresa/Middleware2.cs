namespace JustTest.Middlewaresa
{
    public class Middleware2 : MiddlewareBase
    {
        public Middleware2() 
        {
            id = 2;
        }
        public Middleware2(RequestDelegate next) : base(next) 
        {
            HasTryCatch = false;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Выполняется Middleware2");
            await _next(context);
        }
    }
}
