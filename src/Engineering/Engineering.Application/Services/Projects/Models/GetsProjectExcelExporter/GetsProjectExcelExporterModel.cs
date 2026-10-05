
namespace Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;

public record GetsProjectExcelExporterModel
{
    public long Id { get; set; }
    public long? ProjectTypeId { get; set; }
    public string? ProjectTypeTitle { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long EmployerId { get; set; }
    public string? EmployerName { get; set; } = string.Empty;
    public string? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? SupervisorEngineer { get; set; }
    public string? SupervisorEngineerName { get; set; } = string.Empty;
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; } = string.Empty;
    public long? PlanningAssistantId { get; set; }
    public string? PlanningAssistantName { get; set; } = string.Empty;
    public string? StatusTitle { get; set; } = string.Empty;
    public bool Contractual { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public long? CityId { get; set; }
    public string? City { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public bool HasProduct { get; set; }
};
