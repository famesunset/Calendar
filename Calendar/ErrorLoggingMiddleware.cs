namespace Calendar
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class ErrorLoggingMiddleware
    {
        private readonly static string logPath = $"{DateTime.UtcNow.ToShortDateString()}-exceptions.log";
        private readonly RequestDelegate next;

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
                using (var writter = File.AppendText(logPath))
                {
                    writter.Write($"Exception: {ex.Message}\nTrace:\n{ex.StackTrace}\n");
                }
                throw;
            }
        }
    }
}
