using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.StateChangerFixAssetMachineries;

public record StateChangerFixAssetMachineriesCommand(
    List<FixAssetMachinery> Items,
    bool State
    ) : ICommand<bool?>;
