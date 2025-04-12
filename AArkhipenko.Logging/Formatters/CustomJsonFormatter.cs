using AArkhipenko.Logging.Models;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;

using CoreConsts = AArkhipenko.Core.Consts;

namespace AArkhipenko.Logging.Formatters
{
    /// <summary>
    /// Особенный json форматор сообщений в лог
    /// </summary>
    internal class CustomJsonFormatter : ITextFormatter
    {
        private readonly Formatting _jsonFormatting;

        public CustomJsonFormatter(bool inlineView)
        {
            this._jsonFormatting = inlineView ? Formatting.None : Formatting.Indented;
        }

        /// <inheritdoc/>
        public void Format(LogEvent @event, TextWriter output)
        {
            var logEntry = new LogEntry
            {
                Timestamp = @event.Timestamp,
                LogLevel = @event.Level.ToString(),
                Message = @event.MessageTemplate.Render(@event.Properties),
                Exception = @event.Exception?.ToString()
            };

            foreach (var property in @event.Properties)
            {
                switch (property.Key)
                {
                    case Consts.LogEventConsts.ClassName:
                        {
                            if (logEntry.Action is null)
                            {
                                logEntry.Action = string.Empty;
                            }
                            logEntry.Action = property.Value.ToString() + logEntry.Action;
                            break;
                        }
                    case Consts.LogEventConsts.MethodName:
                        {
                            if(logEntry.Action is null)
                            {
                                logEntry.Action = string.Empty;
                            }
                            logEntry.Action += "." + property.Value.ToString();
                            break;
                        }
                    case CoreConsts.RequestChainId:
                        {
                            if (Guid.TryParse(property.Value.ToString(), out var requestId))
                            {
                                logEntry.RequestId = requestId;
                            }
                            break;
                        }
                    case Consts.LogEventConsts.Method:
                        {
                            logEntry.Method = property.Value.ToString();
                            break;
                        }
                    case Consts.LogEventConsts.Path:
                        {
                            logEntry.Path = property.Value.ToString();
                            break;
                        }
                    case Consts.LogEventConsts.Scope:
                        {
                            if (property.Value is SequenceValue)
                            {
                                var elements = (property.Value as SequenceValue).Elements;
                                var scopes = elements.Select(x => (x as ScalarValue).Value);
                                logEntry.Scope = string.Join(" -> ", scopes);
                            }
                            break;
                        }
                }
            }

            output.Write(JsonConvert.SerializeObject(logEntry, this._jsonFormatting));
            output.Write(Environment.NewLine);
        }
    }
}
