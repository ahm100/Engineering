using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsByNameOrCode;

public record GetsByNameOrCodeRequest(
    string FilterData,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
