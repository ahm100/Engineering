namespace Engineering.Application.Services.FixAssetMachineries.Models.InactiveFixAssetMachinery;

public class InactiveFixAssetMachineryValidator : AbstractValidator<InactiveFixAssetMachineryRequest>
{
    public InactiveFixAssetMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
