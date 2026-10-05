using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjects;

public record GetProjectsRequest(
    string? FilterData,
    long? EmployerId,
    List<long>? CostCenterId,
    long? ProjectTypeId,
    List<long>? CategoryIds,
    long? ProjectManagerId,
    long? PlanningAssistantId,
    long? ThirdPartyId,
    long? SupervisorEngineerId,
    long? AdvisorId,
    long? ImplementationAssistantId,
    long? TechnicalAssistantId,
    ProjectStatus? Status,
    bool? IsActive,
    string[]? OrderBy,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false) : IHttpRequest;