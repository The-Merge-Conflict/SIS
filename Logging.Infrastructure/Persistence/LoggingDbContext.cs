using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logging.Infrastructure.Persistence
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
        }

        public DbSet<Log> Logs => Set<Log>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(log => log.Id);
                entity.Property(log => log.Message).HasMaxLength(2000);
                entity.Property(log => log.CreatedBy).HasMaxLength(200);
                entity.Property(log => log.CreatedAt).IsRequired();
                entity.HasIndex(log => log.CreatedAt);
                entity.HasIndex(log => log.CreatedBy);
            });
        }
    }
}
