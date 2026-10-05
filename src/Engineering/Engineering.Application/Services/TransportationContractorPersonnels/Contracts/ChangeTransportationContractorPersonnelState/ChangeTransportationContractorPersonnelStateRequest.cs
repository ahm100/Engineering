namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;

public record ChangeTransportationContractorPersonnelStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
