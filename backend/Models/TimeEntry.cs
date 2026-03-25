using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class TimeEntry
{
    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid UserId { get; set; }

    public string? Description { get; set; }

    public int DurationMins { get; set; }

    public DateOnly LoggedDate { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
