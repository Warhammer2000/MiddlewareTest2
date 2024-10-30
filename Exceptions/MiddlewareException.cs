// <copyright file="MiddlewareException.cs" company="PlaceholderCompany">
// """
// </copyright>
namespace JustTest.Exceptions
{
    using System;

    /// <summary>
    /// Represents an exception that occurs within a middleware component.
    /// </summary>
    public class MiddlewareException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewareException"/> class with a specified error message and middleware ID.
        /// </summary>
        /// <param name="middlewareId">The identifier of the middleware where the exception occurred.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MiddlewareException(int middlewareId, string message)
            : base(message)
        {
            this.MiddlewareId = middlewareId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewareException"/> class with a specified error message, middleware ID, and inner exception.
        /// </summary>
        /// <param name="middlewareId">The identifier of the middleware where the exception occurred.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MiddlewareException(int middlewareId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.MiddlewareId = middlewareId;
        }

        /// <summary>
        /// Gets the identifier of the middleware where the exception occurred.
        /// </summary>
        public int MiddlewareId { get; }

        /// <summary>
        /// Returns a string representation of the current exception.
        /// </summary>
        /// <returns>A string that represents the current exception.</returns>
        public override string ToString()
        {
            return $"MiddlewareException in Middleware ID {this.MiddlewareId}: {this.Message}{Environment.NewLine}{base.ToString()}";
        }
    }
}