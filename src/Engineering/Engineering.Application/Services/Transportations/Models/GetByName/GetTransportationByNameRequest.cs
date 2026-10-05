
namespace Engineering.Application.Services.Transportations.Models.GetByName;

public record GetTransportationByNameRequest(
    string TransportationName
     ) : IHttpRequest;
