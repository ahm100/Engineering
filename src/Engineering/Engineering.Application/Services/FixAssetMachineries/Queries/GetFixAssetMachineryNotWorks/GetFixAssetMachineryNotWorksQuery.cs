using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineryNotWorks;

public record GetFixAssetMachineryNotWorksQuery(
        List<long>? FixAssetMachineryIds,
        List<long>? MachineryIds,
        FixAssetMachineryType? Type,
        DateTime? FromDate,
        DateTime? ToDate,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<FixAssetMachineryNotWork>>>;