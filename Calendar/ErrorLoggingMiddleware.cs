namespace Calendar
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class ErrorLoggingMiddleware
    {
        private static readonly string LogPath;
        private readonly RequestDelegate next;

        static ErrorLoggingMiddleware()
        {
            var now = DateTime.UtcNow;
            LogPath = $"{now:yyyy-MM-dd}-exceptions.log";
        }

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await using (var writter = File.AppendText(LogPath))
                {
                    var now = DateTime.UtcNow;
                    writter.Write($"[{now:G}]Exception: {ex.Message}\nTrace:\n{ex.StackTrace}\n");
                }
                throw;
            }
        }
    }
}
