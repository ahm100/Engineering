using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.InactiveFixAssetMachinery;

public record InactiveFixAssetMachineryCommand(
    long Id
    ) : ICommand<FixAssetMachinery>;