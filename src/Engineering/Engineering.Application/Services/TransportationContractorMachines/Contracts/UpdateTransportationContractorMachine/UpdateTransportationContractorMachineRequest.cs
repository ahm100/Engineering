namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;

public record UpdateTransportationContractorMachineRequest(
    long Id,
    long ContractorId,
    long MachineTypeId,
    string NumberPlate,
    string? Color,
    string? Vin,
    bool IsActive,
    List<long>? ContractorPersonnels
    ) : IHttpRequest;