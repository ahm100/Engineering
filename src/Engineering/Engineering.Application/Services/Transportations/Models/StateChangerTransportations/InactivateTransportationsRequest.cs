
namespace Engineering.Application.Services.Transportations.Models.StateChangerTransportations;

public record InactivateTransportationsRequest(
    List<long> Ids
    ) : IHttpRequest;
