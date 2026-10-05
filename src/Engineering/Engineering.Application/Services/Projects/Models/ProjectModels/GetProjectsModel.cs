using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record GetProjectsModel
{
    public long Id { get; set; }
    public long? ProjectTypeId { get; set; }
    public string? ProjectTypeTitle { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectEnName { get; set; }
    public string? ProjectCode { get; set; } = string.Empty;
    public string? Prefix { get; set; }
    public long? EmployerId { get; set; }
    public string? EmployerName { get; set; } = string.Empty;
    public string? CategoryId => Category?.Select(x => x.strCategoryId).JoinList();
    public string? CategoryName => Category?.Select(x => x.CategoryName).JoinList();
    public List<GetProjectsCategoryModel>? Category { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public string? CostCenterNames { get; set; } = string.Empty;
    public long? SupervisorEngineer { get; set; }
    public string? SupervisorEngineerName { get; set; } = string.Empty;
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; } = string.Empty;
    public long? PlanningAssistantId { get; set; }
    public string? PlanningAssistantName { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public long? CityId { get; set; }
    public string? CityName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public bool Contractual { get; set; }
    public bool CollectiveService { get; set; }
    public bool IsActive { get; set; }
    public decimal? ApprovedBudget { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public bool HasProduct { get; set; }
    public long? OrganizationId { get; set; }
    public string? Organization { get; set; }
    public bool IsOrganizationUnit { get; set; }
}

public record GetProjectsCategoryModel()
{
    public long CategoryId { get; set; }
    public string strCategoryId => CategoryId.ToString();
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
}
