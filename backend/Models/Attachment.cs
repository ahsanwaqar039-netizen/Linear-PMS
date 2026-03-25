using System;
using System.Collections.Generic;

namespace SmartPms.Api.Models;

public partial class Attachment
{
    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid UploadedBy { get; set; }

    public string FileName { get; set; } = null!;

    public long? FileSize { get; set; }

    public string? MimeType { get; set; }

    public string? StorageKey { get; set; }

    public string Url { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public string Metadata { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Issue1 Issue { get; set; } = null!;

    public virtual User1 UploadedByNavigation { get; set; } = null!;
}
