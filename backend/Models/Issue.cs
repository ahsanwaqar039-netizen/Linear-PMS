using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Issue
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public Guid? AssigneeId { get; set; }

    public Guid ReporterId { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Assignee { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User Reporter { get; set; } = null!;

    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
