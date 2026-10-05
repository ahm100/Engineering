
namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;

public class CreateFixAssetMachineryRateValidator : AbstractValidator<CreateFixAssetMachineryRateRequest>
{
    public CreateFixAssetMachineryRateValidator()
    {
        RuleFor(oo => oo.FixAssetMachineryId).NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleForEach(oo => oo.Rates)
            .NotEmpty().SetValidator(new CreateRateDataRequestValidator())
            .WithError(FixAssetMachineryErrors.InValidRates);
    }
}
