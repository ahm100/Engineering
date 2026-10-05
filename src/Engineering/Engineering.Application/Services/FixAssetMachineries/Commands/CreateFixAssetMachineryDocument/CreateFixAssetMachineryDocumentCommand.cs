using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryDocument;

public record CreateFixAssetMachineryDocumentCommand(
    string Url,
    FixAssetMachinery FixAssetMachinery
    ) : ICommand<FixAssetMachineryDocument>;
