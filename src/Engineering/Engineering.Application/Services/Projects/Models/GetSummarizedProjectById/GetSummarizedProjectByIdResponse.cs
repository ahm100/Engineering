using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;

public record GetSummarizedProjectByIdResponse(
    long Id,
    string ProjectName,
    string? ProjectCode,
    ProjectCostCenterModel? CostCenter,
    ProjectUserModel ProjectManager,
    ProjectUserModel PlanningAssistant,
    List<ImplementationModel> ImplementationAssistants,
    List<TechnicalModel> TechnicalAssistants,
    bool Contractual,
    long? CompanyId,
    string? CompanyNameFa
    );

