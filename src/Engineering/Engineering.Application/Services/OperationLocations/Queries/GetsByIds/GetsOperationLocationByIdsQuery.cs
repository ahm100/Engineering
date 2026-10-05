using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsByIds;

public record GetsOperationLocationByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<OperationLocation>>>;