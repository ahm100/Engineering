
using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetActiveProjects;

public record GetActiveProjectsQuery(
    string? FilterData,
    long? EmployerId,
    long? CostCenterId,
    long? ProjectTypeId,
    long? CategoryId,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? ThirdPartyId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    long? CompanyId,
    bool? Contractual,
    List<ProjectStatus>? Statuses,
    bool CheckThirdParty,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;