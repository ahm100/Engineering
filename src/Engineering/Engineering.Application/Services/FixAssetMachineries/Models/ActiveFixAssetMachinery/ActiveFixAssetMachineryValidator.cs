namespace Engineering.Application.Services.FixAssetMachineries.Models.ActiveFixAssetMachinery;

public class ActiveFixAssetMachineryValidator : AbstractValidator<ActiveFixAssetMachineryRequest>
{
    public ActiveFixAssetMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
