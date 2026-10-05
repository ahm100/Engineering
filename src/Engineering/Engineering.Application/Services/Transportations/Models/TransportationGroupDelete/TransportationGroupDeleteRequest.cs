
namespace Engineering.Application.Services.Transportations.Models.TransportationGroupDelete;

public record TransportationGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
