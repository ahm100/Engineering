
namespace Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContractHeader;

public class DeleteContractorContractHeaderValidator : AbstractValidator<DeleteContractorContractHeaderRequest>
{
    public DeleteContractorContractHeaderValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.ContractorContractId);

    }
}
