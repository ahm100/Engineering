namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.DisableExpert;

public class DisableConsumptionStandardExpertValidator : AbstractValidator<DisableConsumptionStandardExpertRequest>
{
    public DisableConsumptionStandardExpertValidator()
    {
        RuleFor(oo => oo.OperationInfoExpertId).NotNull().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
    }
}
