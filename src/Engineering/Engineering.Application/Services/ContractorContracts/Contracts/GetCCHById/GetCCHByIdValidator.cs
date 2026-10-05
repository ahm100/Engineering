namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;

public class GetCCHByIdValidator : AbstractValidator<GetCCHByIdRequest>
{
    public GetCCHByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.ContractorContractId);
    }
}