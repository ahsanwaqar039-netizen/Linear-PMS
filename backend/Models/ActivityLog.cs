using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class ActivityLog
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Guid? ActorId { get; set; }

    public Guid EntityId { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string Metadata { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User1? Actor { get; set; }

    public virtual Team1 Team { get; set; } = null!;
}
