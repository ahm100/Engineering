
namespace Engineering.Application.Services.FixAssetMachineries.Commands.ActiveFixAssetMachinery;

public class ActiveFixAssetMachineryCommandValidator : AbstractValidator<ActiveFixAssetMachineryCommand>
{
    public ActiveFixAssetMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}