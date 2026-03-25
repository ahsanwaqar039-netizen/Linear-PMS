using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class CommentReaction
{
    public Guid Id { get; set; }

    public Guid CommentId { get; set; }

    public Guid UserId { get; set; }

    public string Emoji { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User1 User { get; set; } = null!;
}
