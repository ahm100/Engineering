using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorks;

public record GetFixAssetMachineryNotWorksRequest(
        List<long>? FixAssetMachineryIds,
        List<long>? MachineryIds,
        FixAssetMachineryType? Type,
        DateTime? StartDate,
        DateTime? EndDate,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
