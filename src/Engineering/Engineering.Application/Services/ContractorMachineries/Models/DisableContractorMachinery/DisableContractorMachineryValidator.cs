namespace Engineering.Application.Services.ContractorMachineries.Models.DisableContractorMachinery;

public class DisableContractorMachineryValidator : AbstractValidator<DisableContractorMachineryRequest>
{
    public DisableContractorMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
