namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.SetCCTotalAmount;

public class SetCCTotalAmountCommandValidator : AbstractValidator<SetCCTotalAmountCommand>
{
    public SetCCTotalAmountCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull().WithError(ContractorContractErrors.ContractorContractIdIsEmpty);
    }
}
