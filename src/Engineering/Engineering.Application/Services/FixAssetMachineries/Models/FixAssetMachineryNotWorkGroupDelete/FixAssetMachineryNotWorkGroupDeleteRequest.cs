
namespace Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryNotWorkGroupDelete;

public record FixAssetMachineryNotWorkGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
