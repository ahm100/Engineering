
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public class ContractorContractHeaderStatusChangerValidator : AbstractValidator<ContractorContractHeaderStatusChangerModelRequest>
{
    public ContractorContractHeaderStatusChangerValidator()
    {
        RuleFor(c => c.Status)
            .IsEnum(CCCmts.ContractorContractStatus);
    }
}
