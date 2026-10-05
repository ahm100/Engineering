using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetNotWorkDocument;

public record DeleteFixAssetNotWorkDocumentCommand(long Id) : ICommand<FixAssetNotWorkDocument>;
