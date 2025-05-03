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
            internal static string Method => "Method";

            /// <summary>
            /// Название заголовка, в котором лежит ИД цепочки запросов
            /// </summary>
            internal static string Path => "Path";

            /// <summary>
            /// Разделы логирования
            /// </summary>
            internal static string Scope => "Scope";

            /// <summary>
            /// Названием метода, из которого вызов
            /// </summary>
            internal static string MethodName => "MethodName";

            /// <summary>
            /// Названием класса, из которого вызов
            /// </summary>
            internal static string ClassName => "ClassName";
        }
    }
}
