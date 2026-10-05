namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.UpdateExpert;

public class UpdateExpertCommandValidator : AbstractValidator<UpdateExpertCommand>
{
    public UpdateExpertCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ExpertStandardErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.ExpertUnitId).NotNull().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.ExpertNumber).GreaterThanOrEqualTo(0).WithError(ExpertStandardErrors.ExpertNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(ExpertStandardErrors.TimeSpantIsEmpty);
    }
}