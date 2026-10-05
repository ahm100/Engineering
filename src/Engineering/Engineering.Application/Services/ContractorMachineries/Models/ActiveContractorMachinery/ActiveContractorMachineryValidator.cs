namespace Engineering.Application.Services.ContractorMachineries.Models.ActiveContractorMachinery;

public class ActiveContractorMachineryValidator : AbstractValidator<ActiveContractorMachineryRequest>
{
    public ActiveContractorMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
