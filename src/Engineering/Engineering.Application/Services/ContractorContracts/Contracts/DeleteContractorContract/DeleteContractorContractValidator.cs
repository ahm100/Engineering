
namespace Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContract;

public class DeleteContractorContractValidator : AbstractValidator<DeleteContractorContractRequest>
{
    public DeleteContractorContractValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.ContractorContractId);
    }
}
