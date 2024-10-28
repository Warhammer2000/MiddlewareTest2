namespace JustTest.Middlewaresa
{
    public class MiddlewareSelector
    {
        private static readonly Lazy<MiddlewareSelector> _instance = new Lazy<MiddlewareSelector>(() => new MiddlewareSelector());
        private readonly Random _random;
        private readonly List<Type> _middlewareTypes;

        private MiddlewareSelector()
        {
            _random = new Random();
            
            _middlewareTypes = new List<Type>
            {
                typeof(Middleware1),
                typeof(Middleware2),
                typeof(Middleware3),
                typeof(Middleware4),
                typeof(Middleware5)
            };
        }
        
        public static MiddlewareSelector Instance => _instance.Value;
        
        public (Type MiddlewareType, bool ShouldThrowException) GetRandomMiddleware()
        {
            int selectedIndex = _random.Next(_middlewareTypes.Count);
            Type selectedMiddlewareType = _middlewareTypes[selectedIndex];
            
            bool shouldThrowException = _random.NextDouble() < 0.5;
            
            return (selectedMiddlewareType, shouldThrowException);
        }
    }

}
