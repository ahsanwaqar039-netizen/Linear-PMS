using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class TeamInvite
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Guid InvitedBy { get; set; }

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public Guid? AcceptedBy { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User1? AcceptedByNavigation { get; set; }

    public virtual User1 InvitedByNavigation { get; set; } = null!;

    public virtual Team1 Team { get; set; } = null!;
}
