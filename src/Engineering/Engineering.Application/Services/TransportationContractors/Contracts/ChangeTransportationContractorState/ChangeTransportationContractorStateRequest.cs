namespace Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;

public record ChangeTransportationContractorStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
