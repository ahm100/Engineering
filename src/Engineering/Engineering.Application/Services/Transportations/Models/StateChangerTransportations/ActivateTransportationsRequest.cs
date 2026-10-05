
namespace Engineering.Application.Services.Transportations.Models.StateChangerTransportations;

public record ActivateTransportationsRequest(
    List<long> Ids
    ) : IHttpRequest;
