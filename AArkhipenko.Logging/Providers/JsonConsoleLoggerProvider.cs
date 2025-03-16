using AArkhipenko.Logging.Loggers;
using Microsoft.Extensions.Logging;

namespace AArkhipenko.Logging.Providers
{
    /// <summary>
    /// Провайдер для <see cref="JsonConsoleLogger"/>
    /// </summary>
    internal class JsonConsoleLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConsoleLoggerProvider"/> class.
        /// </summary>
        public JsonConsoleLoggerProvider()
        { }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new JsonConsoleLogger();
        }

        /// <inheritdoc/>
        public void Dispose()
        { }
    }
}
