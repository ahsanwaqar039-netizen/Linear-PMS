using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class TeamMember1
{
    public Guid TeamId { get; set; }

    public Guid UserId { get; set; }

    public string? Role { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
