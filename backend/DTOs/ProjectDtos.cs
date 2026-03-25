namespace SmartPms.Api.DTOs;
public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid TeamId { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime? CreatedAt { get; set; }
}
public class CreateProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid TeamId { get; set; }
}
public class UpdateProjectDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
