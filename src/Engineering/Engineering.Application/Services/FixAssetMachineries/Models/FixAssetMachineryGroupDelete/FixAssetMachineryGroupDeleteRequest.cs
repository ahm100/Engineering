
namespace Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryGroupDelete;

public record FixAssetMachineryGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
