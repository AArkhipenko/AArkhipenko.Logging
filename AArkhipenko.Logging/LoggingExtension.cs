using AArkhipenko.Logging.Middleware;
using AArkhipenko.Logging.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AArkhipenko.Logging
{
    /// <summary>
	/// Методы расширешения для настройки логирования в приложении
	/// </summary>
	public static class LoggingExtension
    {
        /// <summary>
        /// Добавления настроект логирования
        /// </summary>
        /// <param name="builder"><see cref="ILoggingBuilder"/></param>
        /// <returns><see cref="ILoggingBuilder"/></returns>
        public static ILoggingBuilder AddLoggingMiddleware(this ILoggingBuilder builder)
        {
            //builder.ClearProviders();
            //builder.AddProvider(new JsonConsoleLoggerProvider());
            return builder;
        }

        /// <summary>
        /// Добавление прослойки логирования
        /// </summary>
        /// <param name="builder"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
