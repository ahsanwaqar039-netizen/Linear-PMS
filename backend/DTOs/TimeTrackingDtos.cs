namespace SmartPms.Api.DTOs;
public class TimeLogDto
{
    public Guid Id { get; set; }
    public Guid IssueId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? DurationMinutes { get; set; }
}
public class StartTimerDto
{
    public Guid IssueId { get; set; }
}
