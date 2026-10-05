using Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;

namespace Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;

public record GetsTripExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    string? TripCode,
    string? TripName,
    List<TripExcelEnum>? ExcelFilters,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
