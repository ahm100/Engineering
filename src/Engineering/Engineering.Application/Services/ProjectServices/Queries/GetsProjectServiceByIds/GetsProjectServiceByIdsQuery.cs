using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceByIds;

public record GetsProjectServiceByIdsQuery(
    List<long> Items
    ) : IQuery<List<ProjectService>>;
