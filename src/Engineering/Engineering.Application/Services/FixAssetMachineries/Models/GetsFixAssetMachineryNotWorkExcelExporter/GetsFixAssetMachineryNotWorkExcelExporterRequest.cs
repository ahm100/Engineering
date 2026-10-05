using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;

public record GetsFixAssetMachineryNotWorkExcelExporterRequest(
    List<long>? Ids,
    List<long>? NotWorkIds,
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    FixAssetMachineryType? Type,
    string? NumberPlates,
    DateTime? FromDate,
    DateTime? ToDate,
    string? DriverName,
    string? FilterData,
    bool? IsActive,
    List<FixAssetMachineryNotworkExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
