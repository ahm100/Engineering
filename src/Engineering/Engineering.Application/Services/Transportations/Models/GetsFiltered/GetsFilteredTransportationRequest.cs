
namespace Engineering.Application.Services.Transportations.Models.GetsFiltered;

public record GetsFilteredTransportationRequest(
    string? FilterData,
    string? TransportationName,
    string? TransportationCode,
    bool? IsPassenger,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
