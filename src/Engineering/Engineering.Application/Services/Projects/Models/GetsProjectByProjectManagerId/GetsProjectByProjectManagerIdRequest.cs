using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsProjectByProjectManagerId;

public record GetsProjectByProjectManagerIdRequest(
    List<long> CostCenterIds,
    long ProjectManagerId,
    string? FilterData,
    List<ProjectStatus>? Statuses,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;
