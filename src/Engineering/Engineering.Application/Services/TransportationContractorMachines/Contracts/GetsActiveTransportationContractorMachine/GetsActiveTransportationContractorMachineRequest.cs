namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;

public record GetsActiveTransportationContractorMachineRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
