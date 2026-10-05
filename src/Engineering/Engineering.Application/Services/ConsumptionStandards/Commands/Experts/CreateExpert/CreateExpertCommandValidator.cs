namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.CreateExpert;

public class CreateExpertCommandValidator : AbstractValidator<CreateExpertCommand>
{
    public CreateExpertCommandValidator()
    {
        RuleFor(oo => oo.ExpertUnitId).NotNull().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.ExpertNumber).GreaterThanOrEqualTo(0).WithError(ExpertStandardErrors.ExpertNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(ExpertStandardErrors.TimeSpantIsEmpty);
    }
}
