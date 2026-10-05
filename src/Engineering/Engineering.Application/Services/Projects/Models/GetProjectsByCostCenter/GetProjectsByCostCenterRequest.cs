using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectsByCostCenter;

public record GetProjectsByCostCenterRequest(
    long CostCenterId,
    string? FilterData,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
