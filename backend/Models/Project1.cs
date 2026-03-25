using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Project1
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string Color { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? TargetDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? LeadId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User1 CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();

    public virtual ICollection<Issue1> Issue1s { get; set; } = new List<Issue1>();

    public virtual ICollection<IssueStatus> IssueStatuses { get; set; } = new List<IssueStatus>();

    public virtual User1? Lead { get; set; }

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    public virtual ProjectIssueSequence? ProjectIssueSequence { get; set; }

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual Team1 Team { get; set; } = null!;
}
