using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.DeleteFixAssetMachineryDocument;

public record DeleteFixAssetMachineryDocumentCommand(long Id) : ICommand<FixAssetMachineryDocument>;
