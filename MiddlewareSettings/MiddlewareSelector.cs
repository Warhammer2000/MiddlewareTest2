using JustTest.Middlewaresa;
using System;

namespace JustTest.MiddlewareSettings
{

    public class MiddlewareSelector
    {
        private static readonly Random _random = new Random();
        public static readonly Type SelectedMiddlewareType;
        public static readonly bool ShouldThrowException;

        static MiddlewareSelector()
        {
            var middlewareTypes = new List<Type>
            {
                typeof(Middleware1),
                typeof(Middleware2),
                typeof(Middleware3),
                typeof(Middleware4),
                typeof(Middleware5)
            };

            SelectedMiddlewareType = middlewareTypes[_random.Next(middlewareTypes.Count)];
            ShouldThrowException = _random.NextDouble() < 0.5;
        }
    }

}
