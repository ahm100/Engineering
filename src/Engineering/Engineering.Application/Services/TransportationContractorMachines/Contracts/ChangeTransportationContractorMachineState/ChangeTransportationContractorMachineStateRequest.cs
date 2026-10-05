namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;

public record ChangeTransportationContractorMachineStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
