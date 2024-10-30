// <copyright file="MiddlewareBase.cs" company="PlaceholderCompany">
// """
// </copyright>

namespace JustTest.Middlewaresa
{
    using JustTest.Exceptions;
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base middleware class.
    /// </summary>
    public class MiddlewareBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewareBase"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the request pipeline.</param>
        /// <param name="selector">The middleware selector instance.</param>
        /// <param name="id">The identifier for this middleware instance.</param>
        public MiddlewareBase(RequestDelegate next, MiddlewareSelector selector, int id)
        {
            this.Next = next;
            this.selector = selector;
            this.Id = id;
        }

        /// <summary>
        /// Gets the next delegate in the request pipeline.
        /// </summary>
        protected RequestDelegate Next { get; }

        /// <summary>
        /// The middleware selector to determine the selected middleware.
        /// </summary>
        private readonly MiddlewareSelector selector;

        /// <summary>
        /// Gets the identifier of the middleware.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Executes the middleware operation asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public virtual async Task InvokeAsync(HttpContext context)
        {
            LogBufferHelper.AddLog(context, $"[Middleware {this.Id}] Passing through {this.GetType().Name}");

            var selectedType = MiddlewareSelector.SelectedMiddlewareType;
            var shouldThrowException = MiddlewareSelector.ShouldThrowException;

            if (this.GetType() == selectedType)
            {
                LogBufferHelper.AddLog(context, $"[Middleware {this.Id}] Executing selected {this.GetType().Name}");

                if (shouldThrowException)
                {
                    LogBufferHelper.AddLog(context, $"[Middleware {this.Id}] {this.GetType().Name} generated an exception!");
                    throw new MiddlewareException(this.Id, $"{this.GetType().Name} generated an exception!");
                }

                LogBufferHelper.AddLog(context, $"[Middleware {this.Id}] {this.GetType().Name} completed successfully.");
            }

            await this.Next(context);

            LogBufferHelper.AddLog(context, $"[Middleware {this.Id}] Exiting {this.GetType().Name}");

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(LogBufferHelper.GetLogBuffer(context));
            }
        }
    }
}