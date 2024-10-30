// <copyright file="LogBufferHelper.cs" company="PlaceholderCompany">
// """
// </copyright>

namespace JustTest
{
    using Microsoft.AspNetCore.Http;
    using System.Text;

    /// <summary>
    /// Provides helper methods for managing log buffers in the HTTP context.
    /// </summary>
    public static class LogBufferHelper
    {
        /// <summary>
        /// The key used to store and retrieve the log buffer from the HTTP context.
        /// </summary>
        private const string LogBufferKey = "LogBuffer";

        /// <summary>
        /// Adds a log message to the log buffer in the specified HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context containing the log buffer.</param>
        /// <param name="message">The log message to add to the buffer.</param>
        public static void AddLog(HttpContext context, string message)
        {
            if (!context.Items.ContainsKey(LogBufferKey))
            {
                context.Items[LogBufferKey] = new StringBuilder();
            }

            var logBuffer = context.Items[LogBufferKey] as StringBuilder ?? new StringBuilder();
            logBuffer.AppendLine(message);

            context.Items[LogBufferKey] = logBuffer;
        }

        /// <summary>
        /// Retrieves the log buffer as a string from the specified HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context containing the log buffer.</param>
        /// <returns>The log buffer as a string, or an empty string if no log buffer exists.</returns>
        public static string GetLogBuffer(HttpContext context)
        {
            if (context.Items.ContainsKey(LogBufferKey) && context.Items[LogBufferKey] is StringBuilder logBuffer)
            {
                return logBuffer.ToString();
            }

            return string.Empty;
        }
    }
}