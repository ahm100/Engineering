namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;

public class DeleteContractorContractCommandValidator : AbstractValidator<DeleteContractorContractCommand>
{
    public DeleteContractorContractCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(ContractorContractErrors.InValidContractorContractId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
