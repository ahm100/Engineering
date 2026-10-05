
namespace Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;

public record ActivateFixAssetMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
