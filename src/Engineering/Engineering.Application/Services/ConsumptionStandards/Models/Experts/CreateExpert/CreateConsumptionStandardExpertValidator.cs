namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.CreateExpert;

public class CreateConsumptionStandardExpertValidator : AbstractValidator<CreateConsumptionStandardExpertRequest>
{
    public CreateConsumptionStandardExpertValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.ExpertNumber).GreaterThan(0).WithError(ExpertStandardErrors.ExpertNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotEmpty().WithError(ExpertStandardErrors.TimeSpantIsEmpty);
    }
}
