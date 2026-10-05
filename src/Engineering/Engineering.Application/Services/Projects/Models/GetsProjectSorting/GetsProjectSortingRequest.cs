using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetsProjectSorting;

public record GetsProjectSortingRequest(
    string? FilterData,
    List<ProjectStatus>? Statuses,
    ProjectStatus? Status,
    bool? IsActive,
    List<SortInfo>? SortBy,
    int PageIndex,
    int PageSize,
    bool? HaveCostCenter = true,
    bool? IsOrganizationUnit = false
     ) : IHttpRequest;

public record SortInfo(
    string Id,
    bool Desc
    );