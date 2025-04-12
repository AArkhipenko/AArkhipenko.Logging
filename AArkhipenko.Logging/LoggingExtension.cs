using AArkhipenko.Logging.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Settings.Configuration;
using Serilog.Enrichers.AspNetCore.HttpContext;
using Serilog.Core.Enrichers;

using CoreConsts = AArkhipenko.Core.Consts;

namespace AArkhipenko.Logging
{
    /// <summary>
	/// Методы расширешения для настройки логирования в приложении
	/// </summary>
	public static class LoggingExtension
    {
        /// <summary>
        /// Добавление логирования в консоль
        /// </summary>
        /// <param name="builder"><see cref="ILoggingBuilder"/></param>
        /// <returns><see cref="ILoggingBuilder"/></returns>
        public static ILoggingBuilder AddConsoleLogging(this ILoggingBuilder builder)
        {
            var customFormatter = new CustomJsonFormatter(false);
            var logger = CreateStandardConfiguration()
                .WriteTo.Console(
                    formatter: customFormatter,
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            return builder.AddLogging(logger);
        }

        /// <summary>
        /// Добавление логирования в файл
        /// </summary>
        /// <param name="builder"><see cref="ILoggingBuilder"/></param>
        /// <returns><see cref="ILoggingBuilder"/></returns>
        public static ILoggingBuilder AddFileLogging(this ILoggingBuilder builder)
        {
            var customFormatter = new CustomJsonFormatter(true);
            var logger = CreateStandardConfiguration()
                .WriteTo.File(
                    path: "./stdout",
                    formatter: customFormatter,
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
            return builder.AddLogging(logger);
        }

        /// <summary>
        /// Добавление логирования с ручной настройкой
        /// </summary>
        /// <param name="builder"><see cref="ILoggingBuilder"/></param>
        /// <param name="builder"><inheritdoc cref="IConfiguration" path="/summary"/></param>
        /// <param name="builder"><inheritdoc cref="ConfigurationReaderOptions" path="/summary"/></param>
        /// <returns><see cref="ILoggingBuilder"/></returns>
        public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder, IConfiguration configuration, ConfigurationReaderOptions options)
        {
            var logger = CreateStandardConfiguration()
                .ReadFrom.Configuration(configuration, options)
                .CreateLogger();
            return builder.AddLogging(logger);
        }

        /// <summary>
        /// Создание стандартной конфигурации логирования
        /// </summary>
        /// <returns></returns>
        private static LoggerConfiguration CreateStandardConfiguration()
            => new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext();

        /// <summary>
        /// Добавление логирования
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static ILoggingBuilder AddLogging(this ILoggingBuilder builder, Logger logger)
        {
            builder.ClearProviders();
            builder.AddSerilog(logger);
            return builder;
        }

        /// <summary>
        /// Добавление прослойки логирования
        /// Обогащение информацией из запроса
        /// </summary>
        /// <param name="builder"><see cref="IApplicationBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseSerilogLogContext(options =>
            {
                options.EnrichersForContextFactory = (context) =>
                {
                    var parsedRequestId = Guid.Empty;
                    if (context.Request.Headers.TryGetValue(CoreConsts.RequestChainId, out var requestIdStr))
                    {
                        Guid.TryParse(requestIdStr, out parsedRequestId);
                    }

                    return new[]
                    {
                        new PropertyEnricher(CoreConsts.RequestChainId, parsedRequestId),
                        new PropertyEnricher(Consts.LogEventConsts.Method, context.Request.Method),
                        new PropertyEnricher(Consts.LogEventConsts.Path, context.Request.Path)
                    };
                };
            });
            return builder;
        }
    }
}
