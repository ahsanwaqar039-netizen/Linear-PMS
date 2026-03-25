using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class ActiveTimer
{
    public Guid UserId { get; set; }

    public Guid IssueId { get; set; }

    public DateTime StartedAt { get; set; }

    public string? Description { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
