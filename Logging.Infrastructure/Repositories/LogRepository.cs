using Logging.Application.Interfaces;
using Logging.Domain.Entities;
using Logging.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Logging.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly LoggingDbContext _context;

        public LogRepository(LoggingDbContext context)
        {
            _context = context;
        }

        public async Task<Log> AddAsync(Log log, CancellationToken cancellationToken = default)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync(cancellationToken);
            return log;
        }

        public async Task<Log?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Logs.AsNoTracking().FirstOrDefaultAsync(log => log.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Log>> GetAsync(
            string? createdBy,
            DateTime? fromUtc,
            DateTime? toUtc,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Logs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(createdBy))
            {
                query = query.Where(log => log.CreatedBy == createdBy);
            }

            if (fromUtc.HasValue)
            {
                query = query.Where(log => log.CreatedAt >= fromUtc.Value);
            }

            if (toUtc.HasValue)
            {
                query = query.Where(log => log.CreatedAt <= toUtc.Value);
            }

            return await query
                .OrderByDescending(log => log.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
