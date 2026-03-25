using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPms.Api.DTOs;
using SmartPms.Api.Services;
using System.Security.Claims;
namespace SmartPms.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IssuesController : ControllerBase
{
    private readonly IIssueService _issueService;
    public IssuesController(IIssueService issueService)
    {
        _issueService = issueService;
    }
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<IssueDto>>> GetProjectIssues(Guid projectId)
    {
        return Ok(await _issueService.GetProjectIssuesAsync(projectId, GetUserId()));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<IssueDto>> GetIssue(Guid id)
    {
        var issue = await _issueService.GetIssueByIdAsync(id, GetUserId());
        if (issue == null) return NotFound();
        return Ok(issue);
    }
    [HttpPost]
    public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto createIssueDto)
    {
        try
        {
            var issue = await _issueService.CreateIssueAsync(createIssueDto, GetUserId());
            return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, issue);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIssue(Guid id, UpdateIssueDto updateIssueDto)
    {
        var result = await _issueService.UpdateIssueAsync(id, updateIssueDto, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] string status)
    {
        var result = await _issueService.ChangeStatusAsync(id, status, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
    [HttpPatch("{id}/assign")]
    public async Task<IActionResult> AssignIssue(Guid id, [FromBody] Guid assigneeId)
    {
        var result = await _issueService.AssignIssueAsync(id, assigneeId, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIssue(Guid id)
    {
        var result = await _issueService.DeleteIssueAsync(id, GetUserId());
        if (!result) return NotFound();
        return NoContent();
    }
}