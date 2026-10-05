namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;

public class GetContractGuaranteesValidator : AbstractValidator<GetContractGuaranteesRequest>
{
    public GetContractGuaranteesValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
    }
}
