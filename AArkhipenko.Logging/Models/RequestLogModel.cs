using Microsoft.AspNetCore.Http;

namespace AArkhipenko.Logging.Models
{
    /// <summary>
    /// Поля запроса, которые должны попасть в лог
    /// </summary>
    internal class RequestLogModel
    {
        /// <summary>
		/// Initializes a new instance of the <see cref="RequestLogModel"/> class.
        /// </summary>
        /// <param name="context"><inheritdoc cref="HttpContext" path="/summary"/></param>
        public RequestLogModel(HttpContext context)
        {
            if(context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request.Headers.TryGetValue("RequestId", out var requestIdStr))
            {
                if (Guid.TryParse(requestIdStr, out var parsedRequestId))
                {
                    this.RequestId = parsedRequestId;
                }
            }
            this.Method = context.Request.Method;
            this.Path = context.Request.Path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLogModel"/> class.
        /// </summary>
        private RequestLogModel()
        { }

        /// <summary>
        /// Пустая модель
        /// </summary>
        public static RequestLogModel Empty { get; } = new RequestLogModel();

        /// <summary>
        /// ИД запроса, для отследивания цепочку вызовов
        /// </summary>
        public Guid RequestId { get; } = Guid.Empty;

        /// <summary>
        /// Тип http метода
        /// </summary>
        public string? Method { get; }

        /// <summary>
        /// АПИ
        /// </summary>
        public string? Path { get; }
    }
}
