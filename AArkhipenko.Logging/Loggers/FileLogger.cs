using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AArkhipenko.Logging.Loggers
{

    /// <summary>
    /// Логер для записи в файл
    /// </summary>
    internal class FileLogger : CustomLoggerBase
    {
        private readonly string _filePath;
        static object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="filePath">путь к файлу для записи лога</param>
        /// <param name="model"><inheritdoc cref="RequestLogModel" path="/summary"/></param>
        public FileLogger(string filePath)
            : base(Formatting.None)
        {
            _filePath = filePath;
        }

        /// <inheritdoc/>
        public override void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var text = FormatMessage(logLevel, formatter(state, exception), exception);
            lock (_lock)
            {
                File.AppendAllText(_filePath, text + Environment.NewLine);
            }
        }
    }
}
