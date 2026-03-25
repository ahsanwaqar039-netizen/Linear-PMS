using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class VProjectProgress
{
    public Guid? ProjectId { get; set; }

    public Guid? TeamId { get; set; }

    public string? ProjectName { get; set; }

    public long? TotalIssues { get; set; }

    public long? CompletedIssues { get; set; }

    public long? CancelledIssues { get; set; }

    public long? InProgressIssues { get; set; }

    public long? OverdueIssues { get; set; }

    public decimal? CompletionPct { get; set; }
}
