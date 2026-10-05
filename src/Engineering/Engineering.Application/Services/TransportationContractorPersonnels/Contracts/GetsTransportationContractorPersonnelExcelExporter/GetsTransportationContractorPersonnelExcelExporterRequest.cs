using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;

namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelExporter;

public record GetsTransportationContractorPersonnelExcelExporterRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? MachineTypeIds,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize,
    List<TransportationContractorPersonnelExcelEnum>? ExcelFilters
     ) : IHttpRequest;
