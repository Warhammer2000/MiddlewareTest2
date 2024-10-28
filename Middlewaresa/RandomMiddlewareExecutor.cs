using Serilog;

namespace JustTest.Middlewaresa
{
    public class RandomMiddlewareExecutor
    {
        private readonly IServiceProvider _serviceProvider; 

        public RandomMiddlewareExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MiddlewareBase GetSelectedMiddleware()
        {
            var (selectedMiddlewareType, shouldThrowException) = MiddlewareSelector.Instance.GetRandomMiddleware();
            
            var selectedMiddleware = (MiddlewareBase)_serviceProvider.GetRequiredService(selectedMiddlewareType);
            selectedMiddleware.ShouldThrowException = shouldThrowException;

            Log.Verbose($"Selected middleware: {selectedMiddleware.GetType().Name}, " +
                        $"{(selectedMiddleware.ShouldThrowException ? "Should throw exception" : "Should succeed response")}");

            return selectedMiddleware;
        }
    }
}
