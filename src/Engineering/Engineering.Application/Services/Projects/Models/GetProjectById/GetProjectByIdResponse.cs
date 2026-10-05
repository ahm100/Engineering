using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public record GetProjectByIdResponse()
{
    public long Id { get; set; }
    public long? ProjectTypeId { get; set; }
    public string? ProjectTypeTitle { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectEnName { get; set; }
    public string? ProjectCode { get; set; }
    public string? Prefix { get; set; }
    public long? EmployerId { get; set; }
    public string? EmployerName { get; set; }
    public string? CategoryId => Category?.Select(x => x.strCategoryId).JoinList();
    public string? CategoryName => Category?.Select(x => x.CategoryName).JoinList();
    public List<GetProjectsCategoryModel>? Category { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public List<ProjectCostCenterModel>? CostCenterList { get; set; }
    public long? SupervisorEngineer { get; set; }
    public string? SupervisorEngineerName { get; set; }
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
    public long? PlanningAssistantId { get; set; }
    public string? PlanningAssistantName { get; set; }
    public List<ImplementationModel>? ImplementationAssistants { get; set; }
    public List<TechnicalModel>? TechnicalAssistants { get; set; }
    public ProjectStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public bool? Contractual { get; set; }
    public bool? CollectiveService { get; set; }
    public bool? IsActive { get; set; }
    public bool HasProduct { get; set; }
    public long? OrganizationId { get; set; }
    public string? Organization { get; set; }
    public bool IsOrganizationUnit { get; set; }
    public ProjectStatusModel? ProjectStatus { get; set; }
    public ProjectTypeModel? ProjectType { get; set; }
    public ProjectCostCenterModel? CostCenter { get; set; }
    public ProjectUserModel? Employer { get; set; }
    public ProjectUserModel? Supervisor { get; set; }
    public ProjectUserModel? Advisor { get; set; }
    public ProjectUserModel? ProjectManager { get; set; }
    public ProjectUserModel? PlanningAssistant { get; set; }
    public decimal? ApprovedBudget { get; set; }
    public ProjectCityInfo? ProjectCityInfo { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
}

public class ProjectCityInfo
{
    public long? CityId { get; set; }
    public string? City { get; set; }
    public long? ProvinceId { get; set; }
    public string? Province { get; set; }
    public long? CountryId { get; set; }
    public string? Country { get; set; }
    public string? AddressDescription { get; set; } = string.Empty;
}


public class MyViewCity
{
    public long? Id { get; set; }
    public long? ProvinceId { get; set; }
    public string? Name { get; set; }
}
