namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class Middleware2 : MiddlewareBase
    {
        public Middleware2(RequestDelegate next, MiddlewareSelector selector) : base(next, selector, 2) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            await base.InvokeAsync(context);
        }
    }
}
