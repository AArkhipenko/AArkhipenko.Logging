using AArkhipenko.Logging.Loggers;
using Microsoft.Extensions.Logging;

namespace AArkhipenko.Logging.Providers
{
    /// <summary>
    /// Провайдер для <see cref="FileLogger"/>
    /// </summary>
    internal class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoggerProvider"/> class.
        /// </summary>
        /// <param name="filePath">путь к файлу для записи лога</param>
        public FileLoggerProvider(string filePath)
        {
            this._filePath = filePath;
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this._filePath);
        }

        /// <inheritdoc/>
        public void Dispose()
        { }
    }
}
