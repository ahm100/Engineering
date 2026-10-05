using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryNotWork;

public record DisableFixAssetMachineryNotWorkCommand(
    long Id
    ) : ICommand<FixAssetMachineryNotWork>;