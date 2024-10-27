namespace JustTest.Middlewaresa
{
    public class MiddlewareBase
    {
        public int id;
        public Guid Guid { get; }
        protected readonly RequestDelegate _next;
        public bool HasTryCatch { get; set; }

        public MiddlewareBase()
        {
            id = 505;
        }
        public MiddlewareBase(RequestDelegate next)
        {
            Guid = Guid.NewGuid();
            _next = next;
        }
    }

}
