using Serilog.Events;

namespace AArkhipenko.Logging
{
    /// <summary>
    /// Константы
    /// </summary>
    internal class Consts
    {
        /// <summary>
        /// Константы для разбора <see cref="LogEvent"/>
        /// </summary>
        internal class LogEventConsts
        {
            /// <summary>
            /// Название заголовка, в котором лежит ИД цепочки запросов
            /// </summary>
            internal const string Method = "Method";

            /// <summary>
            /// Название заголовка, в котором лежит ИД цепочки запросов
            /// </summary>
            internal const string Path = "Path";

            /// <summary>
            /// Разделы логирования
            /// </summary>
            internal const string Scope = "Scope";

            /// <summary>
            /// Названием метода, из которого вызов
            /// </summary>
            internal const string MethodName = "MethodName";

            /// <summary>
            /// Названием класса, из которого вызов
            /// </summary>
            internal const string ClassName = "ClassName";
        }
    }
}
