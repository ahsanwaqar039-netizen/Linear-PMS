using SmartPms.Api.DTOs;
namespace SmartPms.Api.Services;
public interface IIssueService
{
    Task<IEnumerable<IssueDto>> GetProjectIssuesAsync(Guid projectId, Guid userId);
    Task<IssueDto?> GetIssueByIdAsync(Guid id, Guid userId);
    Task<IssueDto> CreateIssueAsync(CreateIssueDto createIssueDto, Guid userId);
    Task<bool> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto, Guid userId);
    Task<bool> AssignIssueAsync(Guid id, Guid assigneeId, Guid userId);
    Task<bool> ChangeStatusAsync(Guid id, string status, Guid userId);
    Task<bool> DeleteIssueAsync(Guid id, Guid userId);
}
