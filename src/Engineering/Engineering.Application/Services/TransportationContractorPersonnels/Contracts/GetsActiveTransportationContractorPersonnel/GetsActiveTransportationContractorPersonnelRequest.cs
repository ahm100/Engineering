namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;

public record GetsActiveTransportationContractorPersonnelRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? PersonnelIds,
    List<long>? MachineIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
