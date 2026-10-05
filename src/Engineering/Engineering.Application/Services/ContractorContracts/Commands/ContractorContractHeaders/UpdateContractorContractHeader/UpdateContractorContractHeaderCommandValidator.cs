namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.UpdateContractorContractHeader;

public class UpdateContractorContractHeaderCommandValidator : AbstractValidator<UpdateContractorContractHeaderCommand>
{
    public UpdateContractorContractHeaderCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull().WithError(ContractorContractErrors.ContractorContractRequestIdIsEmpty);

        RuleFor(c => c.CurrencyId)
            .NotNull().WithError(ContractorContractErrors.CurrencyIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
