using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Issue1
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Guid? ParentId { get; set; }

    public Guid StatusId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? DescriptionHtml { get; set; }

    public int IssueNumber { get; set; }

    public Guid? AssigneeId { get; set; }

    public Guid CreatorId { get; set; }

    public decimal? Estimate { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public double SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ActiveTimer> ActiveTimers { get; set; } = new List<ActiveTimer>();

    public virtual User1? Assignee { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User1 Creator { get; set; } = null!;

    public virtual ICollection<CycleIssue> CycleIssues { get; set; } = new List<CycleIssue>();

    public virtual ICollection<Issue1> InverseParent { get; set; } = new List<Issue1>();

    public virtual ICollection<IssueRelation> IssueRelationIssues { get; set; } = new List<IssueRelation>();

    public virtual ICollection<IssueRelation> IssueRelationRelateds { get; set; } = new List<IssueRelation>();

    public virtual ICollection<IssueSubscriber> IssueSubscribers { get; set; } = new List<IssueSubscriber>();

    public virtual ICollection<ModuleIssue> ModuleIssues { get; set; } = new List<ModuleIssue>();

    public virtual Issue1? Parent { get; set; }

    public virtual Project1 Project { get; set; } = null!;

    public virtual IssueStatus Status { get; set; } = null!;

    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
}
