using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Body { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public Guid? ActorId { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User1? Actor { get; set; }

    public virtual User1 User { get; set; } = null!;
}
