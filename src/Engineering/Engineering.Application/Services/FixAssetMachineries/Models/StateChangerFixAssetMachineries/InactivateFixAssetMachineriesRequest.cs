
namespace Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;

public record InactivateFixAssetMachineriesRequest(
    List<long> Ids
    ) : IHttpRequest;
