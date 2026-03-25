using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class UserPreference
{
    public Guid UserId { get; set; }

    public string Theme { get; set; } = null!;

    public bool SidebarCollapsed { get; set; }

    public bool ShowCompletedIssues { get; set; }

    public string DefaultIssueView { get; set; } = null!;

    public string NotificationSettings { get; set; } = null!;

    public string KeyboardShortcuts { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual User1 User { get; set; } = null!;
}
