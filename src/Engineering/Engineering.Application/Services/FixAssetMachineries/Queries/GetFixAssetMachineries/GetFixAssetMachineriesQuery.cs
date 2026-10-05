
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineries;

public record GetFixAssetMachineriesQuery(
    List<long>? Ids,
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
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<FixAssetMachinery>>>;