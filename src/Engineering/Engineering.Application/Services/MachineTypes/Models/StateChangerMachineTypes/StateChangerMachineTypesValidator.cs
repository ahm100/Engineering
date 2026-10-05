
namespace Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;

public class StateChangerMachineTypesValidator : AbstractValidator<StateChangerMachineTypesRequest>
{
    public StateChangerMachineTypesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
