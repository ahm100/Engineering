using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;

namespace Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;

public record GetsSeasonExcelExporterRequest(
    List<long>? Ids,
    string? FilterData,
    long? BranchId,
    string? SeasonCode,
    string? SeasonName,
    bool? IsActive,
    string[]? OrderBy,
    List<SeasonExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
