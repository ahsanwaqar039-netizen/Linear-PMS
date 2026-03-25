using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Cycle
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User1 CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CycleIssue> CycleIssues { get; set; } = new List<CycleIssue>();

    public virtual Project1 Project { get; set; } = null!;
}
