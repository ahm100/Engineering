using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;

namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelExporter;

public record GetsTransportationContractorExcelExporterRequest(
    List<long>? Ids,
    long? ThirdPartyId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize,
    List<TransportationContractorExcelEnum>? ExcelFilters
     ) : IHttpRequest;
