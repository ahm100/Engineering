using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;

namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelExporter;

public record GetsTransportationContractorMachineExcelExporterRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize,
    List<TransportationContractorMachineExcelEnum>? ExcelFilters
     ) : IHttpRequest;
