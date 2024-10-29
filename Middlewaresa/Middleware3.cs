namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class Middleware3 : MiddlewareBase
    {
        public Middleware3(RequestDelegate next, MiddlewareSelector selector) : base(next, selector, 3) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            await base.InvokeAsync(context);
        }
    }


}
