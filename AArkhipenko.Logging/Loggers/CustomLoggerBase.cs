using AArkhipenko.Logging.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AArkhipenko.Logging.Loggers
{
    /// <summary>
	/// База класс кастомного логера
	/// </summary>
	internal abstract class CustomLoggerBase : ILogger, IDisposable
    {
        private MethodLogModel? methodModel = null;
        private RequestLogModel requestmodel = RequestLogModel.Empty;
        private readonly Formatting _jsonFormatting;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLoggerBase"/> class.
        /// </summary>
        /// <param name="jsonFormatting"><see cref="Formatting"/></param>
        /// <param name="contextAccessor"><see cref="IHttpContextAccessor"/></param>
        public CustomLoggerBase(Formatting jsonFormatting)
        {
            _jsonFormatting = jsonFormatting;
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState scopeModel)
        {
            if (scopeModel is not null)
            {
                if (scopeModel is MethodLogModel)
                {
                    methodModel = scopeModel as MethodLogModel;
                }
                else if (scopeModel is RequestLogModel)
                {
                    requestmodel = scopeModel as RequestLogModel;
                }
            } 
            return this;
        }

        /// <inheritdoc/>
        public abstract void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter);

        /// <inheritdoc/>
        public void Dispose() { }

        /// <summary>
        /// Формирование отформатированного сообщения для лога
        /// </summary>
        /// <param name="logLevel">уровень логирования</param>
        /// <param name="message">сообщение для лога</param>
        /// <param name="exception">выброшенное исключение</param>
        /// <returns>строка json</returns>
        protected string FormatMessage(LogLevel logLevel, string message, Exception? exception)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                RequestId = requestmodel.RequestId,
                LogLevel = logLevel.ToString(),
                Method = requestmodel.Method,
                Path = requestmodel.Path,
                Scope = methodModel is null ? "unknown" : $"{methodModel.ClassName}.{methodModel.MethodName}",
                Message = message,
                Exception = exception?.ToString()
            };

            return JsonConvert.SerializeObject(logEntry, _jsonFormatting);
        }
    }
}
