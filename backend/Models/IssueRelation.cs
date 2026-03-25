using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class IssueRelation
{
    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid RelatedId { get; set; }

    public string RelationType { get; set; } = null!;

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User1? CreatedByNavigation { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual Issue1 Related { get; set; } = null!;
}
