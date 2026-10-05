using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetsFixAssetMachineryNotWorkForExcel;

public record GetsFixAssetMachineryNotWorkForExcelQuery(
    List<long>? Ids,
    List<long>? NotWorkIds,
    List<long>? MachineryIds,
    List<long>? ContractorIds,
    FixAssetMachineryType? Type,
    string? NumberPlates,
    DateTime? FromDate,
    DateTime? ToDate,
    List<long>? DriverIds,
    string? DriverFilter,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsFixAssetMachineryNotWorkExcelExporterModel>>>;