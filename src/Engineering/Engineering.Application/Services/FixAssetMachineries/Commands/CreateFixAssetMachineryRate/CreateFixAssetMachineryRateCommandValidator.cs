using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryRate;

public class CreateFixAssetMachineryRateCommandValidator : AbstractValidator<CreateFixAssetMachineryRateCommand>
{
    public CreateFixAssetMachineryRateCommandValidator()
    {
        RuleFor(oo => oo.FixAssetMachinery).NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery);

        RuleForEach(oo => oo.Rates)
            .NotEmpty().SetValidator(new CreateRateDataCommandValidator())
            .WithError(FixAssetMachineryErrors.InValidRates);
    }
}