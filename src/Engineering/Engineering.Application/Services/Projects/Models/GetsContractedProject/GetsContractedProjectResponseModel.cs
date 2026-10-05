
using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetsContractedProject;

public record GetsContractedProjectResponseModel(
    long Id,
    string ProjectName,
    string? ProjectCode,
    string? CategoryId,
    string? CategoryName,
    long? CostCenterId,
    string? CostCenterName,
    long? ProjectManagerId,
    string? ProjectManagerName,
    long? EmployerId,
    string? EmployerName,
    long? PlanningAssistantId,
    string? PlanningAssistantName,
    List<UserProjectTechnicalAssistantModel?> ProjectTechnicalAssistants,
    List<UserProjectImplementationAssistantModel?> ProjectImplementationAssistants,
    string StatusTitle,
    bool Contractual,
    bool CollectiveService,
    bool HasProduct,
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa,
    List<ProjectCategoryModel>? Category,
    List<long>? WarehouseIds
    );
