using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachinery;

public record DisableFixAssetMachineryCommand(
    long Id
    ) : ICommand<FixAssetMachinery>;