using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class VCycleProgress
{
    public Guid? CycleId { get; set; }

    public Guid? ProjectId { get; set; }

    public string? CycleName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public long? TotalIssues { get; set; }

    public long? CompletedIssues { get; set; }

    public decimal? TotalEstimate { get; set; }

    public decimal? CompletedEstimate { get; set; }
}
