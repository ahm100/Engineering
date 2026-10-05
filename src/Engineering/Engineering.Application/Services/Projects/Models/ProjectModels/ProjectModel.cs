using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.ProjectModels;

public record ProjectModel(
    long Id,
    long ProjectTypeId,
    string ProjectTypeTitle,
    string ProjectName,
    string? ProjectCode,
    long? EmployerId,
    string? EmployerName,
    string? CategoryId,
    string? CategoryName,
    long CostCenterId,
    string CostCenterName,
    long? SupervisorEngineer,
    string? SupervisorEngineerName,
    long? AdvisorId,
    string? AdvisorName,
    long? ProjectManagerId,
    string? ProjectManagerName,
    long? PlanningAssistantId,
    string? PlanningAssistantName,
    List<ImplementationModel> ImplementationAssistants,
    List<TechnicalModel> TechnicalAssistants,
    ProjectStatus Status,
    string StatusTitle,
    bool CollectiveService,
    bool IsActive,
    bool Contractual,
    ProjectStatusModel ProjectStatus,
    ProjectTypeModel ProjectType,
    List<ProjectCategoryModel>? Category,
    ProjectCostCenterModel CostCenter,
    ProjectUserModel Employer,
    ProjectUserModel? Supervisor,
    ProjectUserModel? Advisor,
    ProjectUserModel? ProjectManager,
    ProjectUserModel? PlanningAssistant,
    long? CompanyId,
    string? CompanyNameFa,
    Guid PreferentialReferenceCode
    );

