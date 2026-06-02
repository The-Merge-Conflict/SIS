using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SIS.Application.DTOs.Logging;
using SIS.Application.Services.Interfaces;

namespace SIS.Infrastructure.Services
{
    public class ActivityLogClient : IActivityLogClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ActivityLogClient> _logger;

        public ActivityLogClient(HttpClient httpClient, ILogger<ActivityLogClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task WriteAsync(ActivityLogRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync("api/logs", request, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Logging service rejected event {EventType} with status code {StatusCode}.", request.EventType, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Logging service is unavailable while writing event {EventType}.", request.EventType);
            }
        }
    }
}
