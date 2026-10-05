using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetNotWorkDocument;

public record CreateFixAssetNotWorkDocumentCommand(
    string Url,
    FixAssetMachineryNotWork FixAssetNotWork
    ) : ICommand<FixAssetNotWorkDocument>;
