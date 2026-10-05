namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;

public record GetsFilteredTransportationContractorPersonnelRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? PersonnelIds,
    List<long>? MachineTypeIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;