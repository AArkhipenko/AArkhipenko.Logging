﻿using Microsoft.Extensions.Logging;

namespace AArkhipenko.Logging.Wrappers
{
	/// <summary>
	/// Интерфейс обертки для логирования
	/// </summary>
	public interface ILoggerWrapper
	{
		/// <summary>
		/// Объекл логирования
		/// </summary>
		public ILogger Logger { get; }

		/// <summary>
		/// Начало логирования раздела
		/// </summary>
		/// <param name="myCallerName">название метода, из которого выпонен вызов</param>
		/// <param name="callerClassName">название класса, из которого выпонен вызов</param>
		/// <returns><see cref="LoggerWrapperScope"/></returns>
		public LoggerWrapperScope BeginLoggingScope(string? callerMethodName = null, string? callerClassName = null);
	}
}
