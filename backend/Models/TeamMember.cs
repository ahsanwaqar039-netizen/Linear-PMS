using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class TeamMember
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Guid UserId { get; set; }

    public DateTime JoinedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Team1 Team { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
