namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryDocument;

public class CreateFixAssetMachineryDocumentCommandValidator : AbstractValidator<CreateFixAssetMachineryDocumentCommand>
{
    public CreateFixAssetMachineryDocumentCommandValidator()
    {
        RuleFor(oo => oo.FixAssetMachinery).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery);
        RuleFor(oo => oo.Url).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidDocument);
    }
}
