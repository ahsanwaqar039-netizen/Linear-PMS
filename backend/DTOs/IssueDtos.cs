namespace SmartPms.Api.DTOs;
public class IssueDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid ReporterId { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
}
public class CreateIssueDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Status { get; set; } = "Todo";
    public string? Priority { get; set; } = "Medium";
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
}
public class UpdateIssueDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public Guid? AssigneeId { get; set; }
    public DateTime? DueDate { get; set; }
}