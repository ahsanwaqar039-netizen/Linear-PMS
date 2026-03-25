using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPms.Api.DTOs;
using SmartPms.Api.Services;
using System.Security.Claims;
namespace SmartPms.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TimeTrackingController : ControllerBase
{
    private readonly ITimeTrackingService _timeTrackingService;
    public TimeTrackingController(ITimeTrackingService timeTrackingService)
    {
        _timeTrackingService = timeTrackingService;
    }
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    [HttpPost("start")]
    public async Task<ActionResult<TimeLogDto>> StartTimer(StartTimerDto startTimerDto)
    {
        var log = await _timeTrackingService.StartTimerAsync(startTimerDto.IssueId, GetUserId());
        if (log == null) return NotFound("Issue not found or access denied.");
        return Ok(log);
    }
    [HttpPost("stop")]
    public async Task<ActionResult<TimeLogDto>> StopTimer([FromBody] Guid issueId)
    {
        var log = await _timeTrackingService.StopTimerAsync(issueId, GetUserId());
        if (log == null) return NotFound("Active timer not found for this issue.");
        return Ok(log);
    }
    [HttpGet("issue/{issueId}")]
    public async Task<ActionResult<IEnumerable<TimeLogDto>>> GetIssueTimeLogs(Guid issueId)
    {
        return Ok(await _timeTrackingService.GetIssueTimeLogsAsync(issueId, GetUserId()));
    }
} 