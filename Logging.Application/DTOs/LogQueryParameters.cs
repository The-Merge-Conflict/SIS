namespace Logging.Application.DTOs
{
    public class LogQueryParameters
    {
        public string? CreatedBy { get; set; }
        public DateTime? FromUtc { get; set; }
        public DateTime? ToUtc { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
