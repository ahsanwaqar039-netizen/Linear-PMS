using SmartPms.Api.DTOs;
namespace SmartPms.Api.Services;
public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(Guid userId);
    Task<ProjectDto?> GetProjectByIdAsync(Guid id, Guid userId);
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto, Guid userId);
    Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto, Guid userId);
    Task<bool> DeleteProjectAsync(Guid id, Guid userId);
}