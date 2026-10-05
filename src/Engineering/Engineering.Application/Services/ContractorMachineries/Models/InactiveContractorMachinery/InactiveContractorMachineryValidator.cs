namespace Engineering.Application.Services.ContractorMachineries.Models.InactiveContractorMachinery;

public class InactiveContractorMachineryValidator : AbstractValidator<InactiveContractorMachineryRequest>
{
    public InactiveContractorMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
