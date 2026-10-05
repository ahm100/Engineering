namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;

public record CreateTransportationContractorMachineRequest(
    long TransportationContractorId,
    List<CreateTransportationContractorMachineModel> ContractorMachines
    ) : IHttpRequest;

public record CreateTransportationContractorMachineModel(
    long MachineTypeId,
    string NumberPlate,
    string? Color,
    string? Vin,
    List<long>? ContractorPersonnels
    );