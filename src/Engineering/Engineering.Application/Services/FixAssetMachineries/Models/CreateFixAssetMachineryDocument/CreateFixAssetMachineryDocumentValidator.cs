namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryDocument;

public class CreateFixAssetMachineryDocumentValidator : AbstractValidator<CreateFixAssetMachineryDocumentRequest>
{
    public CreateFixAssetMachineryDocumentValidator()
    {
        RuleFor(oo => oo.FixAssetMachineryId).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachineryId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
