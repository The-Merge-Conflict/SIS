using SIS.Application.DTOs.Logging;

namespace SIS.Application.Services.Interfaces
{
    public interface IActivityLogClient
    {
        Task WriteAsync(ActivityLogRequest request, CancellationToken cancellationToken = default);
    }
}
