
namespace Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;

public record StateChangerFixAssetMachineriesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
