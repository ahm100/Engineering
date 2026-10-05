
namespace Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;

public class StateChangerFixAssetMachineriesValidator : AbstractValidator<StateChangerFixAssetMachineriesRequest>
{
    public StateChangerFixAssetMachineriesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
