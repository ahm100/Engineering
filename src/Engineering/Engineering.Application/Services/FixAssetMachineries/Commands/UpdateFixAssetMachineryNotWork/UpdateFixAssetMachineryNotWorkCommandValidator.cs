namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryNotWork;

public class UpdateFixAssetMachineryNotWorkCommandValidator : AbstractValidator<UpdateFixAssetMachineryNotWorkCommand>
{
    public UpdateFixAssetMachineryNotWorkCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
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