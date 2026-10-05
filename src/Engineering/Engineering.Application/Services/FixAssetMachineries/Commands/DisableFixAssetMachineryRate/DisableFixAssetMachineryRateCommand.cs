using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryRate;

public record DisableFixAssetMachineryRateCommand(
    long Id
    ) : ICommand<FixAssetMachineryRate>;