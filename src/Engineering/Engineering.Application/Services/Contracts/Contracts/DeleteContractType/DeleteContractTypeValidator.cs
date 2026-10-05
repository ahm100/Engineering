namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractType;

public class DeleteContractTypeValidator : AbstractValidator<DeleteContractTypeRequest>
{
    public DeleteContractTypeValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}