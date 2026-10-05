using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelExporter;

public record GetsFixAssetMachineryExcelExporterRequest(
    List<long>? Ids,
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    FixAssetMachineryType? Type,
    string? NumberPlates,
    DateTime? FromDate,
    DateTime? ToDate,
    string? DriverName,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    List<FixAssetMachineryExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
