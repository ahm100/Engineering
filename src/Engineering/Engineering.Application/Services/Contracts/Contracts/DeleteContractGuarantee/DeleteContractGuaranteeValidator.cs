namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractGuarantee;

public class DeleteContractGuaranteeValidator : AbstractValidator<DeleteContractGuaranteeRequest>
{
    public DeleteContractGuaranteeValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
