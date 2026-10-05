using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsActiveProjectByCostCenterIds;

public record GetsActiveProjectByCostCenterIdsRequest(
    string? FilterData,
    long? EmployerId,
    List<long>? CostCenterIds,
    List<ProjectStatus>? Statuses,
    long? ProjectTypeId,
    long? CategoryId,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    bool? Contractual,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
