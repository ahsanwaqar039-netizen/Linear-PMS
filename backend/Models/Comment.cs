using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid? ParentId { get; set; }

    public Guid AuthorId { get; set; }

    public string Body { get; set; } = null!;

    public string? BodyHtml { get; set; }

    public bool IsEdited { get; set; }

    public DateTime? EditedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User1 Author { get; set; } = null!;

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual Comment? Parent { get; set; }
}
