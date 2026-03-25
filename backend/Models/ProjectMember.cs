using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class ProjectMember
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Project1 Project { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
