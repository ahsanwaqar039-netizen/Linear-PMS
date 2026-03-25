using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class SavedView
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Guid CreatedBy { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Filters { get; set; } = null!;

    public bool IsShared { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User1 CreatedByNavigation { get; set; } = null!;

    public virtual Team1 Team { get; set; } = null!;
}
