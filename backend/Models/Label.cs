using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Label
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User1? CreatedByNavigation { get; set; }

    public virtual Team1 Team { get; set; } = null!;

    public virtual ICollection<Issue1> Issues { get; set; } = new List<Issue1>();
}
