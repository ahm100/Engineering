namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;

public record GetsFilteredTransportationContractorMachineRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;