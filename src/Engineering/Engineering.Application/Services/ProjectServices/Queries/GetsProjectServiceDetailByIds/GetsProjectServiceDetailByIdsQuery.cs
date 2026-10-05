using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetailByIds;

public record GetsProjectServiceDetailByIdsQuery(
        List<long> Ids,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<ProjectServiceDetail>>>;