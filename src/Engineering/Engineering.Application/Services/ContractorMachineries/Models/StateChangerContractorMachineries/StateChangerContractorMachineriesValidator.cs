
namespace Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;

public class StateChangerContractorMachineriesValidator : AbstractValidator<StateChangerContractorMachineriesRequest>
{
    public StateChangerContractorMachineriesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
