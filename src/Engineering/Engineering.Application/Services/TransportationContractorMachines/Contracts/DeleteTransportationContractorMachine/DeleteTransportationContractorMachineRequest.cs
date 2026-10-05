namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;

public record DeleteTransportationContractorMachineRequest(
    List<long> Ids
    ) : IHttpRequest;
