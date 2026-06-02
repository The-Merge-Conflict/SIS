namespace Logging.Application.DTOs
{
    public class LogDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
