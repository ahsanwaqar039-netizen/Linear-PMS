using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class IssueSubscriber
{
    public Guid IssueId { get; set; }

    public Guid UserId { get; set; }

    public DateTime SubscribedAt { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
