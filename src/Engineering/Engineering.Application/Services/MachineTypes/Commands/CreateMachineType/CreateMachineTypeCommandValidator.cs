namespace Engineering.Application.Services.MachineTypes.Commands.CreateMachineType;

public class CreateMachineTypeCommandValidator : AbstractValidator<CreateMachineTypeCommand>
{
    public CreateMachineTypeCommandValidator()
    {
        RuleFor(oo => oo.MachineTypeTitle).NotEmpty().WithError(MachineTypeErrors.MachineTypeTitleIsEmpty);
        RuleFor(oo => oo.MachineTypeCode).NotEmpty().WithError(MachineTypeErrors.MachineTypeCodeIsEmpty);
        RuleFor(oo => oo.FromWeight).NotNull().WithError(MachineTypeErrors.FromWeightIsEmpty);
        RuleFor(oo => oo.UntilWeight).NotNull().WithError(MachineTypeErrors.UntilWeightIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineTypeErrors.IsActive);
        RuleFor(oo => oo.CabinTypeId).NotNull().WithError(MachineTypeErrors.CabinTypeCodeIsEmpty);
    }
}