using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsFiltered;

public record GetsFilteredTransportationQuery(
    List<long>? Ids,
    string? FilterData,
    string? TransportationName,
    string? TransportationCode,
    bool? IsPassenger,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Transportation>>>;
