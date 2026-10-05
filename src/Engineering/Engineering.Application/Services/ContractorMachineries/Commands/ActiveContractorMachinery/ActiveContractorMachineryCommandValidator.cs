
namespace Engineering.Application.Services.ContractorMachineries.Commands.ActiveContractorMachinery;

public class ActiveContractorMachineryCommandValidator : AbstractValidator<ActiveContractorMachineryCommand>
{
    public ActiveContractorMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}