using Logging.Application.DTOs;

namespace Logging.Application.Interfaces
{
    public interface ILogService
    {
        Task<LogDto> CreateAsync(CreateLogRequest request, CancellationToken cancellationToken = default);
        Task<LogDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<LogDto>> GetAsync(LogQueryParameters query, CancellationToken cancellationToken = default);
    }
}
