using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetsProjectTypeByIds;

public record GetsProjectTypeByIdsQuery(
    List<long> Items
    ) : IQuery<List<ProjectType>>;
