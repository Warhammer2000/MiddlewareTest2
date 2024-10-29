namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class Middleware5 : MiddlewareBase
    {
        public Middleware5(RequestDelegate next, MiddlewareSelector selector) : base(next, selector, 5) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            await base.InvokeAsync(context);
        }
    }


}
