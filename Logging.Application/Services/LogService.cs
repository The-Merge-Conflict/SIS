using Logging.Application.DTOs;
using Logging.Application.Interfaces;
using Logging.Domain.Entities;

namespace Logging.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logs;

        public LogService(ILogRepository logs)
        {
            _logs = logs;
        }

        public async Task<LogDto> CreateAsync(CreateLogRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Message is required.", nameof(request));
            }

            var log = new Log
            {
                Message = BuildMessage(request),
                CreatedBy = string.IsNullOrWhiteSpace(request.CreatedBy) ? "system" : request.CreatedBy.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            var created = await _logs.AddAsync(log, cancellationToken);
            return Map(created);
        }

        public async Task<LogDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var log = await _logs.GetByIdAsync(id, cancellationToken);
            return log == null ? null : Map(log);
        }

        public async Task<IReadOnlyList<LogDto>> GetAsync(LogQueryParameters query, CancellationToken cancellationToken = default)
        {
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = Math.Clamp(query.PageSize, 1, 200);

            var logs = await _logs.GetAsync(
                query.CreatedBy,
                query.FromUtc,
                query.ToUtc,
                page,
                pageSize,
                cancellationToken);

            return logs.Select(Map).ToList();
        }

        private static LogDto Map(Log log)
        {
            return new LogDto
            {
                Id = log.Id,
                Message = log.Message,
                CreatedBy = log.CreatedBy,
                CreatedAt = log.CreatedAt
            };
        }

        private static string BuildMessage(CreateLogRequest request)
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.ServiceName))
            {
                parts.Add($"Service={request.ServiceName.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.EventType))
            {
                parts.Add($"Event={request.EventType.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.LogLevel))
            {
                parts.Add($"Level={request.LogLevel.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.ResourceType))
            {
                parts.Add($"ResourceType={request.ResourceType.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.ResourceId))
            {
                parts.Add($"ResourceId={request.ResourceId.Trim()}");
            }

            if (!string.IsNullOrWhiteSpace(request.CorrelationId))
            {
                parts.Add($"CorrelationId={request.CorrelationId.Trim()}");
            }

            var metadata = parts.Count == 0 ? string.Empty : $"[{string.Join("; ", parts)}] ";
            var payload = string.IsNullOrWhiteSpace(request.Payload) ? string.Empty : $" Payload={request.Payload}";

            return $"{metadata}{request.Message.Trim()}{payload}";
        }
    }
}
