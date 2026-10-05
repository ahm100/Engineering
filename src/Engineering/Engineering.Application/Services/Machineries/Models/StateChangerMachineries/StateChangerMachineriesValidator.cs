
namespace Engineering.Application.Services.Machineries.Models.StateChangerMachineries;

public class StateChangerMachineriesValidator : AbstractValidator<StateChangerMachineriesRequest>
{
    public StateChangerMachineriesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
