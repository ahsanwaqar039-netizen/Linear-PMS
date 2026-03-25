using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartPms.Api.Models;

namespace SmartPms.Api.Data;

public partial class SmartPmsDbContext : DbContext
{
    public SmartPmsDbContext()
    {
    }

    public SmartPmsDbContext(DbContextOptions<SmartPmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveTimer> ActiveTimers { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReaction> CommentReactions { get; set; }

    public virtual DbSet<Cycle> Cycles { get; set; }

    public virtual DbSet<CycleIssue> CycleIssues { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Issue1> Issues1 { get; set; }

    public virtual DbSet<IssueRelation> IssueRelations { get; set; }

    public virtual DbSet<IssueStatus> IssueStatuses { get; set; }

    public virtual DbSet<IssueSubscriber> IssueSubscribers { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleIssue> ModuleIssues { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Project1> Projects1 { get; set; }

    public virtual DbSet<ProjectIssueSequence> ProjectIssueSequences { get; set; }

    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

    public virtual DbSet<SavedView> SavedViews { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Team1> Teams1 { get; set; }

    public virtual DbSet<TeamInvite> TeamInvites { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<TeamMember1> TeamMembers1 { get; set; }

    public virtual DbSet<TimeEntry> TimeEntries { get; set; }

    public virtual DbSet<TimeLog> TimeLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<User1> Users1 { get; set; }

    public virtual DbSet<UserOauthAccount> UserOauthAccounts { get; set; }

    public virtual DbSet<UserPreference> UserPreferences { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<VCycleProgress> VCycleProgresses { get; set; }

    public virtual DbSet<VIssuesFull> VIssuesFulls { get; set; }

    public virtual DbSet<VProjectProgress> VProjectProgresses { get; set; }

    public virtual DbSet<VTeamMember> VTeamMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Smart-PMS;Username=postgres;Password=ahsan66443");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("activity_action", new[] { "created", "updated", "deleted", "assigned", "unassigned", "status_changed", "priority_changed", "moved", "commented", "archived", "restored", "label_added", "label_removed", "member_added", "member_removed", "due_date_set", "due_date_removed", "estimate_set", "attachment_added", "attachment_removed" })
            .HasPostgresEnum("activity_entity_type", new[] { "issue", "project", "cycle", "team", "comment" })
            .HasPostgresEnum("attachment_source", new[] { "upload", "url", "github", "figma", "notion", "drive" })
            .HasPostgresEnum("cycle_status", new[] { "upcoming", "active", "completed" })
            .HasPostgresEnum("invite_status", new[] { "pending", "accepted", "declined", "expired", "revoked" })
            .HasPostgresEnum("issue_priority", new[] { "no_priority", "urgent", "high", "medium", "low" })
            .HasPostgresEnum("issue_type", new[] { "issue", "bug", "feature", "improvement", "task", "subtask" })
            .HasPostgresEnum("notification_type", new[] { "issue_assigned", "issue_mentioned", "issue_status_changed", "issue_comment", "issue_due_soon", "project_member_added", "team_invite", "cycle_started", "cycle_completed" })
            .HasPostgresEnum("project_status", new[] { "active", "paused", "completed", "cancelled", "archived" })
            .HasPostgresEnum("project_visibility", new[] { "private", "team", "public" })
            .HasPostgresEnum("team_role", new[] { "owner", "admin", "member", "viewer" })
            .HasPostgresEnum("time_entry_source", new[] { "manual", "timer" })
            .HasPostgresEnum("user_status", new[] { "active", "inactive", "suspended", "pending_verification" })
            .HasPostgresExtension("citext")
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<ActiveTimer>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("active_timers_pkey");

            entity.ToTable("active_timers");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("started_at");

            entity.HasOne(d => d.Issue).WithMany(p => p.ActiveTimers)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("active_timers_issue_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.ActiveTimer)
                .HasForeignKey<ActiveTimer>(d => d.UserId)
                .HasConstraintName("active_timers_user_id_fkey");
        });

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activity_log_pkey");

            entity.ToTable("activity_log");

            entity.HasIndex(e => e.ActorId, "idx_activity_actor");

            entity.HasIndex(e => new { e.TeamId, e.CreatedAt }, "idx_activity_team").IsDescending(false, true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.Metadata)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.NewValue)
                .HasColumnType("jsonb")
                .HasColumnName("new_value");
            entity.Property(e => e.OldValue)
                .HasColumnType("jsonb")
                .HasColumnName("old_value");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Actor).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("activity_log_actor_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("activity_log_team_id_fkey");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.HasIndex(e => e.IssueId, "idx_attachments_issue");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.Metadata)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.MimeType)
                .HasMaxLength(100)
                .HasColumnName("mime_type");
            entity.Property(e => e.StorageKey).HasColumnName("storage_key");
            entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url");
            entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Issue).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("attachments_issue_id_fkey");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attachments_uploaded_by_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.HasIndex(e => e.AuthorId, "idx_comments_author");

            entity.HasIndex(e => e.IssueId, "idx_comments_issue").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.ParentId, "idx_comments_parent").HasFilter("(parent_id IS NOT NULL)");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.BodyHtml).HasColumnName("body_html");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.EditedAt).HasColumnName("edited_at");
            entity.Property(e => e.IsEdited)
                .HasDefaultValue(false)
                .HasColumnName("is_edited");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_author_id_fkey");

            entity.HasOne(d => d.Issue).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("comments_issue_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comments_parent_id_fkey");
        });

        modelBuilder.Entity<CommentReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_reactions_pkey");

            entity.ToTable("comment_reactions");

            entity.HasIndex(e => new { e.CommentId, e.UserId, e.Emoji }, "comment_reactions_comment_id_user_id_emoji_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Emoji)
                .HasMaxLength(10)
                .HasColumnName("emoji");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("comment_reactions_comment_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CommentReactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("comment_reactions_user_id_fkey");
        });

        modelBuilder.Entity<Cycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cycles_pkey");

            entity.ToTable("cycles");

            entity.HasIndex(e => e.ProjectId, "idx_cycles_project");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Cycles)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cycles_created_by_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Cycles)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("cycles_project_id_fkey");
        });

        modelBuilder.Entity<CycleIssue>(entity =>
        {
            entity.HasKey(e => new { e.CycleId, e.IssueId }).HasName("cycle_issues_pkey");

            entity.ToTable("cycle_issues");

            entity.HasIndex(e => e.IssueId, "idx_cycle_issues_issue");

            entity.Property(e => e.CycleId).HasColumnName("cycle_id");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("added_at");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.CycleIssues)
                .HasForeignKey(d => d.AddedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("cycle_issues_added_by_fkey");

            entity.HasOne(d => d.Cycle).WithMany(p => p.CycleIssues)
                .HasForeignKey(d => d.CycleId)
                .HasConstraintName("cycle_issues_cycle_id_fkey");

            entity.HasOne(d => d.Issue).WithMany(p => p.CycleIssues)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("cycle_issues_issue_id_fkey");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Issues_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Priority)
                .HasMaxLength(50)
                .HasDefaultValueSql("'No Priority'::character varying");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Todo'::character varying");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Assignee).WithMany(p => p.IssueAssignees)
                .HasForeignKey(d => d.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Issues_AssigneeId_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("Issues_ProjectId_fkey");

            entity.HasOne(d => d.Reporter).WithMany(p => p.IssueReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Issues_ReporterId_fkey");
        });

        modelBuilder.Entity<Issue1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issues_pkey");

            entity.ToTable("issues");

            entity.HasIndex(e => e.AssigneeId, "idx_issues_assignee").HasFilter("((deleted_at IS NULL) AND (assignee_id IS NOT NULL))");

            entity.HasIndex(e => e.CreatorId, "idx_issues_creator").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.DueDate, "idx_issues_due_date").HasFilter("((due_date IS NOT NULL) AND (deleted_at IS NULL))");

            entity.HasIndex(e => new { e.ProjectId, e.IssueNumber }, "idx_issues_number");

            entity.HasIndex(e => e.ParentId, "idx_issues_parent").HasFilter("(parent_id IS NOT NULL)");

            entity.HasIndex(e => e.ProjectId, "idx_issues_project").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => new { e.ProjectId, e.StatusId, e.SortOrder }, "idx_issues_sort_order");

            entity.HasIndex(e => e.StatusId, "idx_issues_status").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => new { e.ProjectId, e.IssueNumber }, "issues_project_id_issue_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ArchivedAt).HasColumnName("archived_at");
            entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");
            entity.Property(e => e.CancelledAt).HasColumnName("cancelled_at");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionHtml).HasColumnName("description_html");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Estimate)
                .HasPrecision(6, 2)
                .HasColumnName("estimate");
            entity.Property(e => e.IssueNumber).HasColumnName("issue_number");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Assignee).WithMany(p => p.Issue1Assignees)
                .HasForeignKey(d => d.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("issues_assignee_id_fkey");

            entity.HasOne(d => d.Creator).WithMany(p => p.Issue1Creators)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("issues_creator_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("issues_parent_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Issue1s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("issues_project_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Issue1s)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("issues_status_id_fkey");

            entity.HasMany(d => d.Labels).WithMany(p => p.Issues)
                .UsingEntity<Dictionary<string, object>>(
                    "IssueLabel",
                    r => r.HasOne<Label>().WithMany()
                        .HasForeignKey("LabelId")
                        .HasConstraintName("issue_labels_label_id_fkey"),
                    l => l.HasOne<Issue1>().WithMany()
                        .HasForeignKey("IssueId")
                        .HasConstraintName("issue_labels_issue_id_fkey"),
                    j =>
                    {
                        j.HasKey("IssueId", "LabelId").HasName("issue_labels_pkey");
                        j.ToTable("issue_labels");
                        j.HasIndex(new[] { "LabelId" }, "idx_issue_labels_label");
                        j.IndexerProperty<Guid>("IssueId").HasColumnName("issue_id");
                        j.IndexerProperty<Guid>("LabelId").HasColumnName("label_id");
                    });
        });

        modelBuilder.Entity<IssueRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issue_relations_pkey");

            entity.ToTable("issue_relations");

            entity.HasIndex(e => e.IssueId, "idx_issue_relations_a");

            entity.HasIndex(e => e.RelatedId, "idx_issue_relations_b");

            entity.HasIndex(e => new { e.IssueId, e.RelatedId, e.RelationType }, "issue_relations_issue_id_related_id_relation_type_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.RelatedId).HasColumnName("related_id");
            entity.Property(e => e.RelationType)
                .HasMaxLength(20)
                .HasColumnName("relation_type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.IssueRelations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("issue_relations_created_by_fkey");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueRelationIssues)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("issue_relations_issue_id_fkey");

            entity.HasOne(d => d.Related).WithMany(p => p.IssueRelationRelateds)
                .HasForeignKey(d => d.RelatedId)
                .HasConstraintName("issue_relations_related_id_fkey");
        });

        modelBuilder.Entity<IssueStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issue_statuses_pkey");

            entity.ToTable("issue_statuses");

            entity.HasIndex(e => new { e.ProjectId, e.Name }, "issue_statuses_project_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasDefaultValueSql("'started'::character varying")
                .HasColumnName("category");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#94a3b8'::character varying")
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasDefaultValue((short)0)
                .HasColumnName("position");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Project).WithMany(p => p.IssueStatuses)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("issue_statuses_project_id_fkey");
        });

        modelBuilder.Entity<IssueSubscriber>(entity =>
        {
            entity.HasKey(e => new { e.IssueId, e.UserId }).HasName("issue_subscribers_pkey");

            entity.ToTable("issue_subscribers");

            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SubscribedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("subscribed_at");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueSubscribers)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("issue_subscribers_issue_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.IssueSubscribers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("issue_subscribers_user_id_fkey");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("labels_pkey");

            entity.ToTable("labels");

            entity.HasIndex(e => e.TeamId, "idx_labels_team");

            entity.HasIndex(e => new { e.TeamId, e.Name }, "labels_team_id_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#6366f1'::character varying")
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Labels)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("labels_created_by_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Labels)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("labels_team_id_fkey");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modules_pkey");

            entity.ToTable("modules");

            entity.HasIndex(e => e.ProjectId, "idx_modules_project");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LeadId).HasColumnName("lead_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TargetDate).HasColumnName("target_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModuleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modules_created_by_fkey");

            entity.HasOne(d => d.Lead).WithMany(p => p.ModuleLeads)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("modules_lead_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Modules)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("modules_project_id_fkey");
        });

        modelBuilder.Entity<ModuleIssue>(entity =>
        {
            entity.HasKey(e => new { e.ModuleId, e.IssueId }).HasName("module_issues_pkey");

            entity.ToTable("module_issues");

            entity.HasIndex(e => e.IssueId, "idx_module_issues_issue");

            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("added_at");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.ModuleIssues)
                .HasForeignKey(d => d.AddedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("module_issues_added_by_fkey");

            entity.HasOne(d => d.Issue).WithMany(p => p.ModuleIssues)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("module_issues_issue_id_fkey");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleIssues)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("module_issues_module_id_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "idx_notifications_entity");

            entity.HasIndex(e => new { e.UserId, e.IsRead, e.CreatedAt }, "idx_notifications_user").IsDescending(false, false, true);

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(30)
                .HasColumnName("entity_type");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.ReadAt).HasColumnName("read_at");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Actor).WithMany(p => p.NotificationActors)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("notifications_actor_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Projects_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Projects_CreatedById_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Projects_TeamId_fkey");
        });

        modelBuilder.Entity<Project1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.HasIndex(e => e.TeamId, "idx_projects_team").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => new { e.TeamId, e.Identifier }, "projects_team_id_identifier_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ArchivedAt).HasColumnName("archived_at");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#6366f1'::character varying")
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon)
                .HasMaxLength(10)
                .HasColumnName("icon");
            entity.Property(e => e.Identifier)
                .HasMaxLength(10)
                .HasColumnName("identifier");
            entity.Property(e => e.LeadId).HasColumnName("lead_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TargetDate).HasColumnName("target_date");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Project1CreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("projects_created_by_fkey");

            entity.HasOne(d => d.Lead).WithMany(p => p.Project1Leads)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("projects_lead_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Project1s)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("projects_team_id_fkey");
        });

        modelBuilder.Entity<ProjectIssueSequence>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("project_issue_sequences_pkey");

            entity.ToTable("project_issue_sequences");

            entity.Property(e => e.ProjectId)
                .ValueGeneratedNever()
                .HasColumnName("project_id");
            entity.Property(e => e.LastNumber)
                .HasDefaultValue(0)
                .HasColumnName("last_number");

            entity.HasOne(d => d.Project).WithOne(p => p.ProjectIssueSequence)
                .HasForeignKey<ProjectIssueSequence>(d => d.ProjectId)
                .HasConstraintName("project_issue_sequences_project_id_fkey");
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("project_members_pkey");

            entity.ToTable("project_members");

            entity.HasIndex(e => e.UserId, "idx_project_members_user");

            entity.HasIndex(e => new { e.ProjectId, e.UserId }, "project_members_project_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("joined_at");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project_members_project_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("project_members_user_id_fkey");
        });

        modelBuilder.Entity<SavedView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("saved_views_pkey");

            entity.ToTable("saved_views");

            entity.HasIndex(e => e.CreatedBy, "idx_saved_views_creator");

            entity.HasIndex(e => new { e.TeamId, e.IsShared }, "idx_saved_views_team");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Filters)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("filters");
            entity.Property(e => e.IsShared)
                .HasDefaultValue(false)
                .HasColumnName("is_shared");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SavedViews)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("saved_views_created_by_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.SavedViews)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("saved_views_team_id_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Teams_pkey");

            entity.HasIndex(e => e.Identifier, "Teams_Identifier_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Identifier).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Team1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_pkey");

            entity.ToTable("teams");

            entity.HasIndex(e => e.Slug, "idx_teams_slug").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.Slug, "teams_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LogoUrl).HasColumnName("logo_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(60)
                .HasColumnName("slug");
            entity.Property(e => e.Timezone)
                .HasMaxLength(100)
                .HasDefaultValueSql("'UTC'::character varying")
                .HasColumnName("timezone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.WebsiteUrl).HasColumnName("website_url");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Team1s)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teams_created_by_fkey");
        });

        modelBuilder.Entity<TeamInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_invites_pkey");

            entity.ToTable("team_invites");

            entity.HasIndex(e => e.Token, "idx_team_invites_token");

            entity.HasIndex(e => e.Token, "team_invites_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AcceptedAt).HasColumnName("accepted_at");
            entity.Property(e => e.AcceptedBy).HasColumnName("accepted_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.ExpiresAt)
                .HasDefaultValueSql("(now() + '7 days'::interval)")
                .HasColumnName("expires_at");
            entity.Property(e => e.InvitedBy).HasColumnName("invited_by");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Token)
                .HasDefaultValueSql("encode(gen_random_bytes(32), 'hex'::text)")
                .HasColumnName("token");

            entity.HasOne(d => d.AcceptedByNavigation).WithMany(p => p.TeamInviteAcceptedByNavigations)
                .HasForeignKey(d => d.AcceptedBy)
                .HasConstraintName("team_invites_accepted_by_fkey");

            entity.HasOne(d => d.InvitedByNavigation).WithMany(p => p.TeamInviteInvitedByNavigations)
                .HasForeignKey(d => d.InvitedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("team_invites_invited_by_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamInvites)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("team_invites_team_id_fkey");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_members_pkey");

            entity.ToTable("team_members");

            entity.HasIndex(e => e.TeamId, "idx_team_members_team");

            entity.HasIndex(e => e.UserId, "idx_team_members_user");

            entity.HasIndex(e => new { e.TeamId, e.UserId }, "team_members_team_id_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("joined_at");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("team_members_team_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("team_members_user_id_fkey");
        });

        modelBuilder.Entity<TeamMember1>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.UserId }).HasName("TeamMembers_pkey");

            entity.ToTable("TeamMembers");

            entity.Property(e => e.JoinedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Member'::character varying");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMember1s)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("TeamMembers_TeamId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMember1s)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("TeamMembers_UserId_fkey");
        });

        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("time_entries_pkey");

            entity.ToTable("time_entries");

            entity.HasIndex(e => e.LoggedDate, "idx_time_entries_date");

            entity.HasIndex(e => e.IssueId, "idx_time_entries_issue");

            entity.HasIndex(e => e.UserId, "idx_time_entries_user");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMins).HasColumnName("duration_mins");
            entity.Property(e => e.EndedAt).HasColumnName("ended_at");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.LoggedDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("logged_date");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Issue).WithMany(p => p.TimeEntries)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("time_entries_issue_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TimeEntries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("time_entries_user_id_fkey");
        });

        modelBuilder.Entity<TimeLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TimeLogs_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.StartTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Issue).WithMany(p => p.TimeLogs)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("TimeLogs_IssueId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TimeLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TimeLogs_UserId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<User1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "idx_users_email").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.Username, "idx_users_username").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt).HasColumnName("email_verified_at");
            entity.Property(e => e.LastActiveAt).HasColumnName("last_active_at");
            entity.Property(e => e.Locale)
                .HasMaxLength(10)
                .HasDefaultValueSql("'en'::character varying")
                .HasColumnName("locale");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Timezone)
                .HasMaxLength(100)
                .HasDefaultValueSql("'UTC'::character varying")
                .HasColumnName("timezone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserOauthAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_oauth_accounts_pkey");

            entity.ToTable("user_oauth_accounts");

            entity.HasIndex(e => new { e.Provider, e.ProviderUid }, "user_oauth_accounts_provider_provider_uid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Provider)
                .HasMaxLength(30)
                .HasColumnName("provider");
            entity.Property(e => e.ProviderUid).HasColumnName("provider_uid");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.TokenExpiresAt).HasColumnName("token_expires_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserOauthAccounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_oauth_accounts_user_id_fkey");
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_preferences_pkey");

            entity.ToTable("user_preferences");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.DefaultIssueView)
                .HasMaxLength(20)
                .HasDefaultValueSql("'list'::character varying")
                .HasColumnName("default_issue_view");
            entity.Property(e => e.KeyboardShortcuts)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("keyboard_shortcuts");
            entity.Property(e => e.NotificationSettings)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("notification_settings");
            entity.Property(e => e.ShowCompletedIssues)
                .HasDefaultValue(false)
                .HasColumnName("show_completed_issues");
            entity.Property(e => e.SidebarCollapsed)
                .HasDefaultValue(false)
                .HasColumnName("sidebar_collapsed");
            entity.Property(e => e.Theme)
                .HasMaxLength(20)
                .HasDefaultValueSql("'system'::character varying")
                .HasColumnName("theme");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.UserPreference)
                .HasForeignKey<UserPreference>(d => d.UserId)
                .HasConstraintName("user_preferences_user_id_fkey");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_sessions_pkey");

            entity.ToTable("user_sessions");

            entity.HasIndex(e => e.UserId, "idx_user_sessions_user");

            entity.HasIndex(e => e.RefreshToken, "user_sessions_refresh_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.LastUsedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_used_at");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_sessions_user_id_fkey");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_tokens_pkey");

            entity.ToTable("user_tokens");

            entity.HasIndex(e => e.ExpiresAt, "idx_user_tokens_expires").HasFilter("(used_at IS NULL)");

            entity.HasIndex(e => e.UserId, "idx_user_tokens_user");

            entity.HasIndex(e => e.TokenHash, "user_tokens_token_hash_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.TokenHash).HasColumnName("token_hash");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type");
            entity.Property(e => e.UsedAt).HasColumnName("used_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_tokens_user_id_fkey");
        });

        modelBuilder.Entity<VCycleProgress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_cycle_progress");

            entity.Property(e => e.CompletedEstimate).HasColumnName("completed_estimate");
            entity.Property(e => e.CompletedIssues).HasColumnName("completed_issues");
            entity.Property(e => e.CycleId).HasColumnName("cycle_id");
            entity.Property(e => e.CycleName)
                .HasMaxLength(150)
                .HasColumnName("cycle_name");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TotalEstimate).HasColumnName("total_estimate");
            entity.Property(e => e.TotalIssues).HasColumnName("total_issues");
        });

        modelBuilder.Entity<VIssuesFull>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_issues_full");

            entity.Property(e => e.ArchivedAt).HasColumnName("archived_at");
            entity.Property(e => e.AssigneeAvatar).HasColumnName("assignee_avatar");
            entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");
            entity.Property(e => e.AssigneeName)
                .HasMaxLength(100)
                .HasColumnName("assignee_name");
            entity.Property(e => e.CancelledAt).HasColumnName("cancelled_at");
            entity.Property(e => e.CommentCount).HasColumnName("comment_count");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.CreatorName)
                .HasMaxLength(100)
                .HasColumnName("creator_name");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DescriptionHtml).HasColumnName("description_html");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Estimate)
                .HasPrecision(6, 2)
                .HasColumnName("estimate");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IssueKey).HasColumnName("issue_key");
            entity.Property(e => e.IssueNumber).HasColumnName("issue_number");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectIdentifier)
                .HasMaxLength(10)
                .HasColumnName("project_identifier");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .HasColumnName("project_name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.StatusCategory)
                .HasMaxLength(20)
                .HasColumnName("status_category");
            entity.Property(e => e.StatusColor)
                .HasMaxLength(7)
                .HasColumnName("status_color");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
            entity.Property(e => e.SubtaskCount).HasColumnName("subtask_count");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .HasColumnName("title");
            entity.Property(e => e.TotalLoggedMins).HasColumnName("total_logged_mins");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<VProjectProgress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_project_progress");

            entity.Property(e => e.CancelledIssues).HasColumnName("cancelled_issues");
            entity.Property(e => e.CompletedIssues).HasColumnName("completed_issues");
            entity.Property(e => e.CompletionPct).HasColumnName("completion_pct");
            entity.Property(e => e.InProgressIssues).HasColumnName("in_progress_issues");
            entity.Property(e => e.OverdueIssues).HasColumnName("overdue_issues");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(150)
                .HasColumnName("project_name");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TotalIssues).HasColumnName("total_issues");
        });

        modelBuilder.Entity<VTeamMember>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_team_members");

            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.JoinedAt).HasColumnName("joined_at");
            entity.Property(e => e.LastActiveAt).HasColumnName("last_active_at");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
