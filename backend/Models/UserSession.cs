using System;
using System.Collections.Generic;
using System.Net;

namespace SmartPms.Api.Models;

public partial class UserSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public IPAddress? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUsedAt { get; set; }

    public virtual User1 User { get; set; } = null!;
}
