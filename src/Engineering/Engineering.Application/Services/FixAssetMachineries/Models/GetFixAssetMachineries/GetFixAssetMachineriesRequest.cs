using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineries;

public record GetFixAssetMachineriesRequest(
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
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
