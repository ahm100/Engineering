using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsOperationLocation;

public record GetsOperationLocationQuery(
    string? FilterData,
    string? PublicName,
    string? PublicCode,
    List<long>? Ids,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationLocation>>>;