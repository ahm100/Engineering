namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorkById;

public record GetFixAssetMachineryNotWorkByIdRequest(
    long Id
     ) : IHttpRequest;
