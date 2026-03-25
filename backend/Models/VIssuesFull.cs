using System;
using System.Collections.Generic;
namespace SmartPms.Api.Models;
public partial class VIssuesFull
{
    public Guid? Id { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? StatusId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DescriptionHtml { get; set; }
    public int? IssueNumber { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? CreatorId { get; set; }
    public decimal? Estimate { get; set; }
    public DateOnly? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public double? SortOrder { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? ProjectIdentifier { get; set; }
    public string? ProjectName { get; set; }
    public Guid? TeamId { get; set; }
    public string? IssueKey { get; set; }
    public string? StatusName { get; set; }
    public string? StatusColor { get; set; }
    public string? StatusCategory { get; set; }
    public string? AssigneeName { get; set; }
    public string? AssigneeAvatar { get; set; }
    public string? CreatorName { get; set; }
    public long? CommentCount { get; set; }
    public long? SubtaskCount { get; set; }
    public long? TotalLoggedMins { get; set; }
}
