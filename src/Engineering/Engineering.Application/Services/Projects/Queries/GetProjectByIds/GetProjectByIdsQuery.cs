using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIds;

public record GetProjectByIdsQuery(
    List<long> Ids,
    bool HaveCostCenter,
    bool IsOrganizationUnit
    ) : IQuery<List<Project>>;
