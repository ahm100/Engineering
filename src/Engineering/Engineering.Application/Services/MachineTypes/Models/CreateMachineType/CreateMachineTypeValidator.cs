namespace Engineering.Application.Services.MachineTypes.Models.CreateMachineType;

public class CreateMachineTypeValidator : AbstractValidator<CreateMachineTypeRequest>
{
    public CreateMachineTypeValidator()
    {
        RuleFor(oo => oo.MachineTypeTitle).NotEmpty().WithError(MachineTypeErrors.MachineTypeTitleIsEmpty);
        RuleFor(oo => oo.MachineTypeCode).NotEmpty().WithError(MachineTypeErrors.MachineTypeCodeIsEmpty);
        RuleFor(oo => oo.FromWeight).NotNull().WithError(MachineTypeErrors.FromWeightIsEmpty);
        RuleFor(oo => oo.UntilWeight).NotNull().WithError(MachineTypeErrors.UntilWeightIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineTypeErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.CabinTypeCode).NotNull().WithError(MachineTypeErrors.CabinTypeCodeIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(MachineTypeErrors.CabinTypeCodeMustBeGreaterThanZero);
        RuleFor(oo => oo.MachineTypeTitle).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.MachineTypeCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}