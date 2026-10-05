namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetNotWorkDocument;

public class CreateFixAssetNotWorkDocumentCommandValidator : AbstractValidator<CreateFixAssetNotWorkDocumentCommand>
{
    public CreateFixAssetNotWorkDocumentCommandValidator()
    {
        RuleFor(oo => oo.FixAssetNotWork).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachineryNotwork);
        RuleFor(oo => oo.Url).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidDocument);
    }
}
