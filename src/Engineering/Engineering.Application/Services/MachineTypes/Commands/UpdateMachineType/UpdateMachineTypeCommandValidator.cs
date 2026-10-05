namespace Engineering.Application.Services.MachineTypes.Commands.UpdateMachineType;

public class UpdateMachineTypeCommandValidator : AbstractValidator<UpdateMachineTypeCommand>
{
    public UpdateMachineTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineTypeErrors.IdIsEmpty);
        RuleFor(oo => oo.MachineTypeTitle).NotEmpty().WithError(MachineTypeErrors.MachineTypeTitleIsEmpty);
        RuleFor(oo => oo.MachineTypeCode).NotEmpty().WithError(MachineTypeErrors.MachineTypeCodeIsEmpty);
        RuleFor(oo => oo.FromWeight).NotNull().WithError(MachineTypeErrors.FromWeightIsEmpty);
        RuleFor(oo => oo.UntilWeight).NotNull().WithError(MachineTypeErrors.UntilWeightIsEmpty);
        RuleFor(oo => oo.CabinTypeId).NotNull().WithError(MachineTypeErrors.CabinTypeCodeIsEmpty);
    }
}