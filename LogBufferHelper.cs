namespace JustTest
{
    using Microsoft.AspNetCore.Http;
    using System.Text;

    public static class LogBufferHelper
    {
        private const string LogBufferKey = "LogBuffer";

        public static void AddLog(HttpContext context, string message)
        {
            if (!context.Items.ContainsKey(LogBufferKey))
            {
                context.Items[LogBufferKey] = new StringBuilder();
            }

            var logBuffer = (StringBuilder)context.Items[LogBufferKey];
            logBuffer.AppendLine(message);
        }

        public static string GetLogBuffer(HttpContext context)
        {
            if (context.Items.ContainsKey(LogBufferKey))
            {
                return context.Items[LogBufferKey].ToString();
            }
            return string.Empty;
        }
    }
}
