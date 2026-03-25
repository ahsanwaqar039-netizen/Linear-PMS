using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Project
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CreatedById { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Team Team { get; set; } = null!;
}
