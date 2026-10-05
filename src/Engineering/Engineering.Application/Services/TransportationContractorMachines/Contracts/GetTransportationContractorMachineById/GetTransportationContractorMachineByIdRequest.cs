
namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;

public record GetTransportationContractorMachineByIdRequest(
    long Id
    ) : IHttpRequest;
