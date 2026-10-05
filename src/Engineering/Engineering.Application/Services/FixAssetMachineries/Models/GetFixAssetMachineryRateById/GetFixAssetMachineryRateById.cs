namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryRateById;

public record GetFixAssetMachineryRateByIdRequest(
    long Id
     ) : IHttpRequest;
