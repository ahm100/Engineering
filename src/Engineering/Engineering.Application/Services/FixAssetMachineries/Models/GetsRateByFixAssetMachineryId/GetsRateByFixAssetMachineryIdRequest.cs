namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsRateByFixAssetMachineryId;

public record GetsRateByFixAssetMachineryIdRequest(
        long FixAssetMachineryId,
        DateTime? StartDate,
        DateTime? EndDate,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
