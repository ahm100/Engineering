namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;

public record ActiveTransportationContractorMachineRequest(
    long Id
    ) : IHttpRequest;
