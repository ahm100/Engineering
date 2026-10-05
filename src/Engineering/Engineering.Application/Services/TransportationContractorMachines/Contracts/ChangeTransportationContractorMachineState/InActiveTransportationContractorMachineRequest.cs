namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;

public record InActiveTransportationContractorMachineRequest(
    long Id
    ) : IHttpRequest;
