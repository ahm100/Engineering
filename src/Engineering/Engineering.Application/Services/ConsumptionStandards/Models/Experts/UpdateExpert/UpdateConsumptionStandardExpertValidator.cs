namespace Engineering.Application.Services.ConsumptionStandards.Models.Experts.UpdateExpert;

public class UpdateConsumptionStandardExpertValidator : AbstractValidator<UpdateConsumptionStandardExpertRequest>
{
    public UpdateConsumptionStandardExpertValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.OperationInfoExpertId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.ExpertNumber).GreaterThan(0).WithError(ExpertStandardErrors.ExpertNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(ExpertStandardErrors.TimeSpantIsEmpty);
    }
}
