namespace Engineering.Application.Services.ConsumptionStandards.Queries.Experts.ExpertsByOperationInfoId;

public class ExpertsByOperationInfoIdQueryValidator : AbstractValidator<ExpertsByOperationInfoIdQuery>
{
    public ExpertsByOperationInfoIdQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().WithError(ExpertStandardErrors.OprationInfoIdIsEmpty);
    }
}