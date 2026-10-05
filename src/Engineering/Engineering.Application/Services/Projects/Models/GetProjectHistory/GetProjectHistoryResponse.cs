using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectHistory;
public record GetProjectHistoryResponse(
    List<GetProjectHistoryModel> Data,
    int RowCount
    );

public record GetProjectHistoryModel()
{
    public long Id { get; set; }
    public long? ProjectTypeId { get; set; }
    public string? ProjectTypeTitle { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? Prefix { get; set; }
    public long? EmployerId { get; set; }
    public string? EmployerName { get; set; }
    public string? CategoryId => Category?.Select(x => x.strCategoryId).JoinList();
    public string? CategoryName => Category?.Select(x => x.strCategoryId).JoinList();
    public List<GetProjectsCategoryModel>? Category { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? SupervisorEngineer { get; set; }
    public string? SupervisorEngineerName { get; set; }
    public long? AdvisorId { get; set; }
    public string? AdvisorName { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
    public long? PlanningAssistantId { get; set; }
    public string? PlanningAssistantName { get; set; }
    public ProjectStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public bool? Contractual { get; set; }
    public bool? CollectiveService { get; set; }
    public bool? IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
}

