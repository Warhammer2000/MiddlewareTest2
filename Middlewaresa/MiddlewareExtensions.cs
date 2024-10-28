namespace JustTest.Middlewaresa
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSelectedMiddlewareExecutor(this IApplicationBuilder builder, MiddlewareBase selectedMiddleware)
        {
            return builder.UseMiddleware<SelectedMiddlewareExecutor>(selectedMiddleware);
        }
        public static IApplicationBuilder UseRandomMiddlewareExecutor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RandomMiddlewareExecutor>();
        }
        public static IApplicationBuilder UseMiddleware1(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware1>();
        }

        public static IApplicationBuilder UseMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware2>();
        }

        public static IApplicationBuilder UseMiddleware3(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware3>();
        }

        public static IApplicationBuilder UseMiddleware4(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware4>();
        }

        public static IApplicationBuilder UseMiddleware5(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware5>();
        }
    }
}
