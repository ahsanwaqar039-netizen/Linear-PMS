using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TeamMember1> TeamMember1s { get; set; } = new List<TeamMember1>();
}
