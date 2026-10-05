namespace Engineering.Application.Services.MachineTypes.Queries.GetDuplicateMachineType;

public class GetDuplicateMachineTypeQueryValidator : AbstractValidator<GetDuplicateMachineTypeQuery>
{
    public GetDuplicateMachineTypeQueryValidator()
    {
        RuleFor(oo => oo.MachineTypeTitle).NotEmpty().WithError(MachineTypeErrors.MachineTypeTitleIsEmpty);
        RuleFor(oo => oo.UntilWeight).NotEmpty().WithError(MachineTypeErrors.UntilWeightIsEmpty);
        RuleFor(oo => oo.FromWeight).NotEmpty().WithError(MachineTypeErrors.FromWeightIsEmpty);
        RuleFor(oo => oo.CabinTypeCode).NotEmpty().WithError(MachineTypeErrors.CabinTypeCodeIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(MachineTypeErrors.CabinTypeCodeMustBeGreaterThanZero);
    }
}
