
namespace Engineering.Application.Services.Transportations.Models.GetsActive;

public record GetsActiveTransportationRequest(
    string? FilterData,
    string? TransportationCode,
    string? TransportationName,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
