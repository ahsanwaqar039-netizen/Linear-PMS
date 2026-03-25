using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPms.Api.DTOs;
using SmartPms.Api.Services;
using System.Security.Claims;
namespace SmartPms.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        return Ok(await _projectService.GetUserProjectsAsync(GetUserId()));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id, GetUserId());
        if (project == null) return NotFound();
        return Ok(project);
    }
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto createProjectDto)
    {
        try
        {
            var project = await _projectService.CreateProjectAsync(createProjectDto, GetUserId());
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var result = await _projectService.UpdateProjectAsync(id, updateProjectDto, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var result = await _projectService.DeleteProjectAsync(id, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
}