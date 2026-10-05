using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryNotWork;

public record UpdateFixAssetMachineryNotWorkCommand(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    string? Description
    ) : ICommand<FixAssetMachineryNotWork>;