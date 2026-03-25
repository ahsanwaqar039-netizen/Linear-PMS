using SmartPms.Api.DTOs;
namespace SmartPms.Api.Services;
public interface ITimeTrackingService
{
    Task<TimeLogDto?> StartTimerAsync(Guid issueId, Guid userId);
    Task<TimeLogDto?> StopTimerAsync(Guid issueId, Guid userId);
    Task<IEnumerable<TimeLogDto>> GetIssueTimeLogsAsync(Guid issueId, Guid userId);
}