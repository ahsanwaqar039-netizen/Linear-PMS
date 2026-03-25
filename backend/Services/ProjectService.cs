using SmartPms.Api.DTOs;
using SmartPms.Api.Models;
using SmartPms.Api.Repositories;
namespace SmartPms.Api.Services;
public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _projectRepo;
    private readonly IRepository<TeamMember> _teamMemberRepo;
    public ProjectService(IRepository<Project> projectRepo, IRepository<TeamMember> teamMemberRepo)
    {
        _projectRepo = projectRepo;
        _teamMemberRepo = teamMemberRepo;
    }
    public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync(Guid userId)
    {
        var userTeams = await _teamMemberRepo.FindAsync(tm => tm.UserId == userId);
        var teamIds = userTeams.Select(tm => tm.TeamId).ToList();
        var projects = await _projectRepo.FindAsync(p => teamIds.Contains(p.TeamId));
        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            TeamId = p.TeamId,
            CreatedById = p.CreatedById,
            CreatedAt = p.CreatedAt
        });
    }
    public async Task<ProjectDto?> GetProjectByIdAsync(Guid id, Guid userId)
    {
        var project = await _projectRepo.GetByIdAsync(id);
        if (project == null) return null;
        var isMember = (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == project.TeamId)).Any();
        if (!isMember) return null;
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            TeamId = project.TeamId,
            CreatedById = project.CreatedById,
            CreatedAt = project.CreatedAt
        };
    }
    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto, Guid userId)
    {
        var isMember = (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == createProjectDto.TeamId)).Any();
        if (!isMember)
        {
             throw new UnauthorizedAccessException("You are not a member of this team.");
        }
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = createProjectDto.Name,
            Description = createProjectDto.Description,
            TeamId = createProjectDto.TeamId,
            CreatedById = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await _projectRepo.AddAsync(project);
        await _projectRepo.SaveChangesAsync();
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            TeamId = project.TeamId,
            CreatedById = project.CreatedById,
            CreatedAt = project.CreatedAt
        };
    }
    public async Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto, Guid userId)
    {
        var project = await _projectRepo.GetByIdAsync(id);
        if (project == null) return false;

        var isMember = (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == project.TeamId)).Any();
        if (!isMember) return false;

        project.Name = updateProjectDto.Name;
        project.Description = updateProjectDto.Description;
        project.UpdatedAt = DateTime.UtcNow;

        _projectRepo.Update(project);
        return await _projectRepo.SaveChangesAsync();
    }
    public async Task<bool> DeleteProjectAsync(Guid id, Guid userId)
    {
        var project = await _projectRepo.GetByIdAsync(id);
        if (project == null) return false;

        var isMember = (await _teamMemberRepo.FindAsync(tm => tm.UserId == userId && tm.TeamId == project.TeamId)).Any();
        if (!isMember) return false;

        _projectRepo.Remove(project);
        return await _projectRepo.SaveChangesAsync();
    }
}