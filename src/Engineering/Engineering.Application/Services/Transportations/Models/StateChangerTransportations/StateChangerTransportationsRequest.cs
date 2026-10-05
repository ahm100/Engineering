
namespace Engineering.Application.Services.Transportations.Models.StateChangerTransportations;

public record StateChangerTransportationsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
