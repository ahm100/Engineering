using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetActiveProjects;

public record GetActiveProjectsRequest(
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
    bool? Contractual,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
