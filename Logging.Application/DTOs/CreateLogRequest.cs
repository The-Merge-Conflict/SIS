namespace Logging.Application.DTOs
{
    public class CreateLogRequest
    {
        public string ServiceName { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string LogLevel { get; set; } = "Information";
        public string Message { get; set; } = string.Empty;
        public string? ResourceType { get; set; }
        public string? ResourceId { get; set; }
        public string? CorrelationId { get; set; }
        public string? Payload { get; set; }
        public string? CreatedBy { get; set; }
    }
}
