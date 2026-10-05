namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryNotWork;

public class CreateFixAssetMachineryNotWorkCommandValidator : AbstractValidator<CreateFixAssetMachineryNotWorkCommand>
{
    public CreateFixAssetMachineryNotWorkCommandValidator()
    {
        RuleFor(oo => oo.FixAssetMachinery).NotEmpty().WithError(FixAssetMachineryErrors.InValidFixAssetMachinery);

        RuleFor(oo => oo.StartDate)
            .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThanOrEqualTo(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThanOrEqualTo(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);
    }
}