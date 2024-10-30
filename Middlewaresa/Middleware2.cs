﻿// <copyright file="Middleware2.cs" company="PlaceholderCompany">
// """
// </copyright>

namespace JustTest.Middlewaresa
{
    using JustTest.MiddlewareSettings;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the middleware with ID 2.
    /// </summary>
    public class Middleware2 : MiddlewareBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Middleware2"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the request pipeline.</param>
        /// <param name="selector">The middleware selector instance.</param>
        public Middleware2(RequestDelegate next, MiddlewareSelector selector)
            : base(next, selector, 1)
        {
        }

        /// <summary>
        /// Executes the middleware operation asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            await base.InvokeAsync(context);
        }
    }
}