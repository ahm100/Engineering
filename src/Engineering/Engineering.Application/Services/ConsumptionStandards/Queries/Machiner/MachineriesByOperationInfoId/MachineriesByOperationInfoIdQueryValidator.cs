namespace Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineriesByOperationInfoId;

public class MachineriesByOperationInfoIdQueryValidator : AbstractValidator<MachineriesByOperationInfoIdQuery>
{
    public MachineriesByOperationInfoIdQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().WithError(MachineryStandardErrors.OprationInfoIdIsEmpty);
    }
}