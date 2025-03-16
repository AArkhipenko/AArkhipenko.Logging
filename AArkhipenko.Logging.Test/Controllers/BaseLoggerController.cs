using AArkhipenko.Logging.Models;
using AArkhipenko.Logging.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AArkhipenko.Logging.Test.Controllers
{
    public abstract class BaseLoggerController: ControllerBase, ILoggerWrapper
    {
        private readonly ILogger _logger;

        protected BaseLoggerController(ILogger logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
		public ILogger Logger { get => this._logger; }

        /// <inheritdoc/>
        public LoggerWrapperScope BeginLoggingScope([CallerMemberName] string? callerMethodName = null, string? callerClassName = null)
        {
            var scopeModel = new MethodLogModel
            {
                ClassName = callerClassName ?? this.GetType().Name,
                MethodName = callerMethodName ?? "unknown",
            };

            return new LoggerWrapperScope(this._logger, scopeModel);
        }
    }
}
