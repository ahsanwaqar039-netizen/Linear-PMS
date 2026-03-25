using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class IssueStatus
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string? Icon { get; set; }

    public string Category { get; set; } = null!;

    public short Position { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Issue1> Issue1s { get; set; } = new List<Issue1>();

    public virtual Project1 Project { get; set; } = null!;
}
