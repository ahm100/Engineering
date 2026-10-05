using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsProjectSorting;

public record GetsProjectSortingModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectTypeId { get; set; }
    public string? ProjectTypeTitle { get; set; } = string.Empty;
    public long EmployerId { get; set; }
    public string? EmployerName { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long? SupervisorEngineer { get; set; }
    public string? SupervisorEngineerName { get; set; } = string.Empty;
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; } = string.Empty;
    public long? PlanningAssistantId { get; set; }
    public string? PlanningAssistantName { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public string StatusDesc => Status.GetEnumDescription();
    public bool IsActive { get; set; }
};
