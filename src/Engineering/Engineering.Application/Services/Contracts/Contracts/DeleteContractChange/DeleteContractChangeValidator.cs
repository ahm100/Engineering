namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractChange;

public class DeleteContractChangeValidator : AbstractValidator<DeleteContractChangeRequest>
{
    public DeleteContractChangeValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
