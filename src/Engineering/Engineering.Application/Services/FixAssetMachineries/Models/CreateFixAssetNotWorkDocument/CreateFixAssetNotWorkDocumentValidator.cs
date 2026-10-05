namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetNotWorkDocument;

public class CreateFixAssetNotWorkDocumentValidator : AbstractValidator<CreateFixAssetNotWorkDocumentRequest>
{
    public CreateFixAssetNotWorkDocumentValidator()
    {
        RuleFor(oo => oo.FixAssetNotWorkId).NotNull().NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachineryNotWorkId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
