
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;

public class GetContractorContractHeaderByIdValidator : AbstractValidator<GetContractorContractHeaderByIdRequest>
{
    public GetContractorContractHeaderByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.ContractorContractId);
    }
}
