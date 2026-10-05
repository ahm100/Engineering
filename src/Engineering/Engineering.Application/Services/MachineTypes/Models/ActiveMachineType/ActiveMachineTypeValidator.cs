namespace Engineering.Application.Services.MachineTypes.Models.ActiveMachineType;

public class ActiveMachineTypeValidator : AbstractValidator<ActiveMachineTypeRequest>
{
    public ActiveMachineTypeValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}
