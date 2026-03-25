using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class VTeamMember
{
    public Guid? TeamId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? JoinedAt { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime? LastActiveAt { get; set; }
}
