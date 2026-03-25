using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Team1
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public string? WebsiteUrl { get; set; }

    public string Timezone { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual User1 CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<Project1> Project1s { get; set; } = new List<Project1>();

    public virtual ICollection<SavedView> SavedViews { get; set; } = new List<SavedView>();

    public virtual ICollection<TeamInvite> TeamInvites { get; set; } = new List<TeamInvite>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
