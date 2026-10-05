using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.ActiveFixAssetMachinery;

public record ActiveFixAssetMachineryCommand(
    long Id
    ) : ICommand<FixAssetMachinery>;