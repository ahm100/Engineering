using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsOperationLocationChild;

public record GetsOperationLocationChildQuery(
    long Id,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;