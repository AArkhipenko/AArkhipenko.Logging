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
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ID запроса (нужен для отслеживания последовательности вызовов)
        /// </summary>
        public Guid RequestId { get; set; }

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
        /// Раздел логирования
        /// </summary>
        public string Scope { get; set; } = string.Empty;

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
