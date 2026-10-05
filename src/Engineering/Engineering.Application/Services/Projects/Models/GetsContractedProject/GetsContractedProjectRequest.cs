using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsContractedProject;

public record GetsContractedProjectRequest(
    List<long>? CostCenterIds,
    ProjectStatus? Status,
    string? FilterData,
    long? EmployerId,
    long? ProjectTypeId,
    long? CategoryId,
    bool? IsActive,
    string[]? OrderBy,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool HaveCostCenter,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
