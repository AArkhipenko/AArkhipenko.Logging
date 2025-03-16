using Newtonsoft.Json;

namespace AArkhipenko.Logging.Models
{
    /// <summary>
    /// Итоговая модель данных, которая попадает в консоль
    /// </summary>
    internal class LogEntry
    {
        /// <summary>
        /// Временная метка
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// ID запроса (нужен для отслеживания последовательности вызовов)
        /// </summary>
        public Guid RequestId { get; set; } = Guid.Empty;

        /// <summary>
        /// Уровень логирования
        /// </summary>
        public string LogLevel { get; set; } = string.Empty;

        /// <summary>
        /// Тип http запроса
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Method { get; set; }

        /// <summary>
        /// АПИ
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Path { get; set; }

        /// <summary>
        /// Метод, который вызывается
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Action { get; set; }

        /// <summary>
        /// Раздел логирования
        /// </summary>
        public string Scope { get; set; } = "unknown";

        /// <summary>
        /// Сообщение для лога
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение исключения
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Exception { get; set; }
    }
}
