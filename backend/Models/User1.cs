using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class User1
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? Bio { get; set; }

    public string Timezone { get; set; } = null!;

    public string Locale { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? EmailVerifiedAt { get; set; }

    public DateTime? LastActiveAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ActiveTimer? ActiveTimer { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CycleIssue> CycleIssues { get; set; } = new List<CycleIssue>();

    public virtual ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();

    public virtual ICollection<Issue1> Issue1Assignees { get; set; } = new List<Issue1>();

    public virtual ICollection<Issue1> Issue1Creators { get; set; } = new List<Issue1>();

    public virtual ICollection<IssueRelation> IssueRelations { get; set; } = new List<IssueRelation>();

    public virtual ICollection<IssueSubscriber> IssueSubscribers { get; set; } = new List<IssueSubscriber>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<Module> ModuleCreatedByNavigations { get; set; } = new List<Module>();

    public virtual ICollection<ModuleIssue> ModuleIssues { get; set; } = new List<ModuleIssue>();

    public virtual ICollection<Module> ModuleLeads { get; set; } = new List<Module>();

    public virtual ICollection<Notification> NotificationActors { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Project1> Project1CreatedByNavigations { get; set; } = new List<Project1>();

    public virtual ICollection<Project1> Project1Leads { get; set; } = new List<Project1>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<SavedView> SavedViews { get; set; } = new List<SavedView>();

    public virtual ICollection<Team1> Team1s { get; set; } = new List<Team1>();

    public virtual ICollection<TeamInvite> TeamInviteAcceptedByNavigations { get; set; } = new List<TeamInvite>();

    public virtual ICollection<TeamInvite> TeamInviteInvitedByNavigations { get; set; } = new List<TeamInvite>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();

    public virtual ICollection<UserOauthAccount> UserOauthAccounts { get; set; } = new List<UserOauthAccount>();

    public virtual UserPreference? UserPreference { get; set; }

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
}
