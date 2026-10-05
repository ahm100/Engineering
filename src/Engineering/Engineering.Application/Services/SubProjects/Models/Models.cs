using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Models;

public record SubProjectManager(long Id, string? FullName);

public class SubProjectDetails
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public string Code { get; set; } = string.Empty;
    public long SequenceNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubProjectType Type { get; set; }
    public string? Description { get; set; }
    public long? ManagerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public SubProjectStatus Status { get; set; }
    public DateTime Created { get; set; }
    public long CreatorId { get; set; }
    public DateTime? Updated { get; set; }
    public long? UpdaterId { get; set; }
    public ParentProjectDetails ParentProject { get; set; } = new();
}

public class ParentProjectDetails
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public long? EmployerId { get; set; }
    public long? CompanyId { get; set; }
    public long? OrganizationId { get; set; }
    public long? CityId { get; set; }
}
