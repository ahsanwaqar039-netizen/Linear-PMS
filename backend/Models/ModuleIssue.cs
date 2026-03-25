using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class ModuleIssue
{
    public Guid ModuleId { get; set; }

    public Guid IssueId { get; set; }

    public Guid? AddedBy { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual User1? AddedByNavigation { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;
}
