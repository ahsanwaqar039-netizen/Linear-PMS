using SmartPms.Api.DTOs;
using SmartPms.Api.Models;
using SmartPms.Api.Repositories;
namespace SmartPms.Api.Services;
public class IssueService : IIssueService
{
    private readonly IRepository<Issue> _issueRepo;
    private readonly IRepository<Project> _projectRepo;
    private readonly IRepository<TeamMember> _teamMemberRepo;
    public IssueService(IRepository<Issue> issueRepo, IRepository<Project> projectRepo, IRepository<TeamMember> teamMemberRepo)
    {
        _issueRepo = issueRepo;
        _projectRepo = projectRepo;
        _teamMemberRepo = teamMemberRepo;
    }
    private async Task<bool> HasProjectAccess(Guid projectId, Guid userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId);
        if (project == null) return false;
        return (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == project.TeamId)).Any();
    }
    public async Task<IEnumerable<IssueDto>> GetProjectIssuesAsync(Guid projectId, Guid userId)
    {
        if (!await HasProjectAccess(projectId, userId)) return Enumerable.Empty<IssueDto>();
        var issues = await _issueRepo.FindAsync(i => i.ProjectId == projectId);
        return issues.Select(i => MapToDto(i));
    }
    public async Task<IssueDto?> GetIssueByIdAsync(Guid id, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(id);
        if (issue == null) return null;
        if (!await HasProjectAccess(issue.ProjectId, userId)) return null;
        return MapToDto(issue);
    }
    public async Task<IssueDto> CreateIssueAsync(CreateIssueDto createIssueDto, Guid userId)
    {
        if (!await HasProjectAccess(createIssueDto.ProjectId, userId))
            throw new UnauthorizedAccessException("You do not have access to this project.");
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = createIssueDto.Title,
            Description = createIssueDto.Description,
            Status = createIssueDto.Status ?? "Todo",
            Priority = createIssueDto.Priority ?? "Medium",
            ProjectId = createIssueDto.ProjectId,
            AssigneeId = createIssueDto.AssigneeId,
            ReporterId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _issueRepo.AddAsync(issue);
        await _issueRepo.SaveChangesAsync();
        return MapToDto(issue);
    }
    public async Task<bool> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(id);
        if (issue == null || !await HasProjectAccess(issue.ProjectId, userId)) return false;
        if (updateIssueDto.Title != null) issue.Title = updateIssueDto.Title;
        if (updateIssueDto.Description != null) issue.Description = updateIssueDto.Description;
        if (updateIssueDto.Status != null) issue.Status = updateIssueDto.Status;
        if (updateIssueDto.Priority != null) issue.Priority = updateIssueDto.Priority;
        if (updateIssueDto.AssigneeId.HasValue) issue.AssigneeId = updateIssueDto.AssigneeId;
        if (updateIssueDto.DueDate.HasValue) issue.DueDate = updateIssueDto.DueDate;
        issue.UpdatedAt = DateTime.UtcNow;
        _issueRepo.Update(issue);
        return await _issueRepo.SaveChangesAsync();
    }
    public async Task<bool> AssignIssueAsync(Guid id, Guid assigneeId, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(id);
        if (issue == null || !await HasProjectAccess(issue.ProjectId, userId)) return false;
        issue.AssigneeId = assigneeId;
        issue.UpdatedAt = DateTime.UtcNow;
        _issueRepo.Update(issue);
        return await _issueRepo.SaveChangesAsync();
    }
    public async Task<bool> ChangeStatusAsync(Guid id, string status, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(id);
        if (issue == null || !await HasProjectAccess(issue.ProjectId, userId)) return false;
        issue.Status = status;
        issue.UpdatedAt = DateTime.UtcNow;
        _issueRepo.Update(issue);
        return await _issueRepo.SaveChangesAsync();
    }
    public async Task<bool> DeleteIssueAsync(Guid id, Guid userId)
    {
        var issue = await _issueRepo.GetByIdAsync(id);
        if (issue == null || !await HasProjectAccess(issue.ProjectId, userId)) return false;
        _issueRepo.Remove(issue);
        return await _issueRepo.SaveChangesAsync();
    }
    private IssueDto MapToDto(Issue i) => new IssueDto
    {
        Id = i.Id,
        Title = i.Title,
        Description = i.Description,
        Status = i.Status,
        Priority = i.Priority,
        ProjectId = i.ProjectId,
        AssigneeId = i.AssigneeId,
        ReporterId = i.ReporterId,
        DueDate = i.DueDate,
        CreatedAt = i.CreatedAt
    };
}