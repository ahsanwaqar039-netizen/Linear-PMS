using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class CycleIssue
{
    public Guid CycleId { get; set; }

    public Guid IssueId { get; set; }

    public Guid? AddedBy { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual User1? AddedByNavigation { get; set; }

    public virtual Cycle Cycle { get; set; } = null!;

    public virtual Issue1 Issue { get; set; } = null!;
}
