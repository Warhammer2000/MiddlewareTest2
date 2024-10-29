namespace JustTest.Exceptions
{
    public class MiddlewareException : Exception
    {
        public int MiddlewareId { get; }

        public MiddlewareException(int middlewareId, string message)
            : base(message)
        {
            MiddlewareId = middlewareId;
        }
        
        public MiddlewareException(int middlewareId, string message, Exception innerException)
            : base(message, innerException)
        {
            MiddlewareId = middlewareId;
        }
        
        public override string ToString()
        {
            return $"MiddlewareException in Middleware ID {MiddlewareId}: {Message}\n{base.ToString()}";
        }
    }
}
