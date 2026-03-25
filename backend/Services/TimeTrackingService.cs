using SmartPms.Api.DTOs;
using SmartPms.Api.Models;
using SmartPms.Api.Repositories;
namespace SmartPms.Api.Services;
public class TimeTrackingService : ITimeTrackingService
{
    private readonly IRepository<TimeLog> _timeLogRepo;
    private readonly IRepository<Issue> _issueRepo;
    private readonly IRepository<Project> _projectRepo;
    private readonly IRepository<TeamMember> _teamMemberRepo;
    public TimeTrackingService(IRepository<TimeLog> timeLogRepo, IRepository<Issue> issueRepo, IRepository<Project> projectRepo, IRepository<TeamMember> teamMemberRepo)
    {
        _timeLogRepo = timeLogRepo;
        _issueRepo = issueRepo;
        _projectRepo = projectRepo;
        _teamMemberRepo = teamMemberRepo;
    }
    private async Task<bool> HasIssueAccess(Guid issueId, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(issueId);
        if (issue == null) return false;
        var project = await _projectRepo.GetByIdAsync(issue.ProjectId);
        if (project == null) return false;
        return (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == project.TeamId)).Any();
    }
    public async Task<TimeLogDto?> StartTimerAsync(Guid issueId, Guid userId)
    {
        if (!await HasIssueAccess(issueId, userId)) return null;
        var activeTimer = (await _timeLogRepo.FindAsync(t => t.UserId == userId && t.IssueId == issueId && t.EndTime == null)).FirstOrDefault();
        if (activeTimer != null) return MapToDto(activeTimer);
        var timeLog = new TimeLog
        {
            Id = Guid.NewGuid(),
            IssueId = issueId,
            UserId = userId,
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _timeLogRepo.AddAsync(timeLog);
        await _timeLogRepo.SaveChangesAsync();
        return MapToDto(timeLog);
    }
    public async Task<TimeLogDto?> StopTimerAsync(Guid issueId, Guid userId)
    {
        if (!await HasIssueAccess(issueId, userId)) return null;
        var timeLog = (await _timeLogRepo.FindAsync(t => t.UserId == userId && t.IssueId == issueId && t.EndTime == null)).FirstOrDefault();
        if (timeLog == null) return null;
        timeLog.EndTime = DateTime.UtcNow;
        timeLog.DurationMinutes = (int)(timeLog.EndTime.Value - timeLog.StartTime).TotalMinutes;
        timeLog.UpdatedAt = DateTime.UtcNow;
        _timeLogRepo.Update(timeLog);
        await _timeLogRepo.SaveChangesAsync();
        return MapToDto(timeLog);
    }
    public async Task<IEnumerable<TimeLogDto>> GetIssueTimeLogsAsync(Guid issueId, Guid userId)
    {
        if (!await HasIssueAccess(issueId, userId)) return Enumerable.Empty<TimeLogDto>();
        var logs = await _timeLogRepo.FindAsync(t => t.IssueId == issueId);
        return logs.Select(l => MapToDto(l));
    }
    private TimeLogDto MapToDto(TimeLog l) => new TimeLogDto
    {
        Id = l.Id,
        IssueId = l.IssueId,
        UserId = l.UserId,
        StartTime = l.StartTime,
        EndTime = l.EndTime,
        DurationMinutes = l.DurationMinutes
    };
}