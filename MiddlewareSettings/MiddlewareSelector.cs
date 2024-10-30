// <copyright file="MiddlewareSelector.cs" company="PlaceholderCompany">
// """
// </copyright>

namespace JustTest.MiddlewareSettings
{
    using JustTest.Middlewaresa;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides functionality to select a middleware type and determine if an exception should be thrown.
    /// </summary>
    public class MiddlewareSelector
    {
        /// <summary>
        /// Gets a value indicating whether an exception should be thrown in the selected middleware.
        /// </summary>
        public static readonly bool ShouldThrowException;

        /// <summary>
        /// Gets the selected middleware type.
        /// </summary>
        public static readonly Type SelectedMiddlewareType;

        /// <summary>
        /// A random number generator for selecting middleware and exception probability.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Initializes static members of the <see cref="MiddlewareSelector"/> class.
        /// </summary>
        static MiddlewareSelector()
        {
            var middlewareTypes = new List<Type>
            {
                typeof(Middleware1),
                typeof(Middleware2),
                typeof(Middleware3),
                typeof(Middleware4),
                typeof(Middleware5),
            };

            int middlewareRandomIndex = Random.Next(middlewareTypes.Count);

            SelectedMiddlewareType = middlewareTypes[middlewareRandomIndex];
            ShouldThrowException = Random.NextDouble() < 0.5;
        }
    }
}