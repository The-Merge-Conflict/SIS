using Logging.Domain.Entities;


namespace Logging.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<Log> AddAsync(Log log, CancellationToken ct = default);
        Task<Log?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Log>> GetAsync(string? createdBy, DateTime? fromUtc,
                                          DateTime? toUtc, int page, int pageSize,
                                          CancellationToken ct = default);
    }

}