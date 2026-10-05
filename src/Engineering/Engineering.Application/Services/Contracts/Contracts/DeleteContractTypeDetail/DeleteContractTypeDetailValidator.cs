namespace Engineering.Application.Services.Contracts.Contracts.DeleteContractTypeDetail;

public class DeleteContractTypeDetailValidator
    : AbstractValidator<DeleteContractTypeDetailRequest>
{
    public DeleteContractTypeDetailValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}