using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Queries.GetProjects;

public record GetProjectsQuery(
    List<long>? Ids,
    string? FilterData,
    ProjectStatus? Status,
    List<long>? CategoryIds,
    List<long>? CostCenterId,
    long? AdvisorId,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? ThirdPartyId,
    long? SupervisorEngineerId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    long? EmployerId,
    long? ProjectTypeId,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    List<ProjectStatus>? Statuses,
    bool CheckThirdParty,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetProjectsModel>>>;