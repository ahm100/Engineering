using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryNotWork;

public record CreateFixAssetMachineryNotWorkCommand(
    FixAssetMachinery FixAssetMachinery,
    DateTime StartDate,
    DateTime EndDate,
    string? Description
    ) : ICommand<FixAssetMachineryNotWork?>;