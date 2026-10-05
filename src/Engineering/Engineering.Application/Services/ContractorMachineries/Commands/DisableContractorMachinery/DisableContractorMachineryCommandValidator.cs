namespace Engineering.Application.Services.ContractorMachineries.Commands.DisableContractorMachinery;

public class DisableContractorMachineryCommandValidator : AbstractValidator<DisableContractorMachineryCommand>
{
    public DisableContractorMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}