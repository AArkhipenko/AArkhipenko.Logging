using AArkhipenko.Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AArkhipenko.Logging.Middleware
{
    /// <summary>
    /// Средний слой логирования запросов
    /// </summary>
    internal class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
		/// Initializes a new instance of the <see cref="RequestLoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/></param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/></param>
        /// <returns><see cref="Task"/></returns>
        public async Task Invoke(HttpContext context)
        {
            var model = new RequestLogModel(context);
            using(var scope = this._logger.BeginScope(model))
            {
                await _next.Invoke(context);
            }
        }
    }
}
