namespace Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;

public class GetContractGuaranteeByIdValidator : AbstractValidator<GetContractGuaranteeByIdRequest>
{
    public GetContractGuaranteeByIdValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
