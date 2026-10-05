using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;

namespace Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;

public record GetsTransportationExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? TransportationName,
    string? TransportationCode,
    bool? IsPassenger,
    List<TransportationExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
