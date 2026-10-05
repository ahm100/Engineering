namespace Engineering.Application.Services.ContractorMachineries.Commands.InactiveContractorMachinery;

public class InactiveContractorMachineryCommandValidator : AbstractValidator<InactiveContractorMachineryCommand>
{
    public InactiveContractorMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}