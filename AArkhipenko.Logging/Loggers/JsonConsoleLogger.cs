using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AArkhipenko.Logging.Loggers
{
    /// <summary>
    /// Логер для записи в консоль в формате json
    /// </summary>
    internal class JsonConsoleLogger : CustomLoggerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConsoleLogger"/> class.
        /// </summary>
        public JsonConsoleLogger()
            : base(Formatting.Indented) { }

        /// <inheritdoc/>
        public override void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var text = FormatMessage(logLevel, formatter(state, exception), exception);
            Console.WriteLine(text);
        }
    }
}
