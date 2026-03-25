using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Issue> IssueAssignees { get; set; } = new List<Issue>();

    public virtual ICollection<Issue> IssueReporters { get; set; } = new List<Issue>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TeamMember1> TeamMember1s { get; set; } = new List<TeamMember1>();

    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
