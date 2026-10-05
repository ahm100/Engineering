using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;

public record GetsMachineryByIdsQuery(
    List<long> ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Machinery?>>>;