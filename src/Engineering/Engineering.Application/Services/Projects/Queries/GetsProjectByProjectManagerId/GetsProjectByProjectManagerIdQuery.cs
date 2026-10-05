using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsProjectByProjectManagerId;

public record GetsProjectByProjectManagerIdQuery(
    List<long> CostCenterIds,
    long ProjectManagerId,
    string? FilterData,
    long? CompanyId,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Project>>>;
