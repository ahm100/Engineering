namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateContractorContractHeader;

public class CreateContractorContractHeaderCommandValidator : AbstractValidator<CreateContractorContractHeaderCommand>
{
    public CreateContractorContractHeaderCommandValidator()
    {
        RuleFor(c => c.ContractorId)
            .NotNull().WithError(ContractorContractErrors.ContractorIdIsEmpty)
            .NotNull().WithError(ContractorContractErrors.ContractorIdIsEmpty);

        RuleFor(c => c.CurrencyId)
            .NotNull().WithError(ContractorContractErrors.CurrencyIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

    }
}
