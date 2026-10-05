namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryById;

public record GetFixAssetMachineryByIdRequest(
    long Id
     ) : IHttpRequest;
