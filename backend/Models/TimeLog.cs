using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class TimeLog
{
    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid UserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? DurationMinutes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Issue Issue { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
