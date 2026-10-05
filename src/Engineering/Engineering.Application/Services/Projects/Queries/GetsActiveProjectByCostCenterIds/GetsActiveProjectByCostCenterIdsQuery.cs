
using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsActiveProjectByCostCenterIds;

public record GetsActiveProjectByCostCenterIdsQuery(
    string? FilterData,
    long? EmployerId,
    List<long>? CostCenterIds,
    long? ProjectTypeId,
    long? CategoryId,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    long? CompanyId,
    bool? Contractual,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;
