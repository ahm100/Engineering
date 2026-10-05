using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsActive;

public record GetsActiveTransportationQuery(
    string? FilterData,
    string? TransportationCode,
    string? TransportationName,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Transportation>>>;