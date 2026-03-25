using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class ProjectIssueSequence
{
    public Guid ProjectId { get; set; }

    public int LastNumber { get; set; }

    public virtual Project1 Project { get; set; } = null!;
}
