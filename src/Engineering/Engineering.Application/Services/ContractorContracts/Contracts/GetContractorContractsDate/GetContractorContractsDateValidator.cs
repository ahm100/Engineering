
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;

public class GetContractorContractsDateValidator : AbstractValidator<GetContractorContractsDateRequest>
{
    public GetContractorContractsDateValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(c => c.ContractorId)
            .IsPositive(GlobalCmts.ContractorId);
    }
}
