namespace JustTest.Middlewaresa
{
    public class Middleware5 : MiddlewareBase
    {
        public Middleware5()
        {
            id = 5;
        }
        public Middleware5(RequestDelegate next) : base(next)
        {
            HasTryCatch = false;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Выполняется Middleware5");
            await _next(context);
        }
    }


}
