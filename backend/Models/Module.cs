using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Module
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? TargetDate { get; set; }

    public Guid? LeadId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User1 CreatedByNavigation { get; set; } = null!;

    public virtual User1? Lead { get; set; }

    public virtual ICollection<ModuleIssue> ModuleIssues { get; set; } = new List<ModuleIssue>();

    public virtual Project1 Project { get; set; } = null!;
}
