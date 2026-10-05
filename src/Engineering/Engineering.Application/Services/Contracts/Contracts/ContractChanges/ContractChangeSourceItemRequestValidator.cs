namespace Engineering.Application.Services.Contracts.Contracts.ContractChanges;

public class ContractChangeSourceItemRequestValidator
    : AbstractValidator<ContractChangeSourceItemRequest>
{
    public ContractChangeSourceItemRequestValidator()
    {
        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);
        RuleFor(oo => oo.SourceId)
            .IsPositive(ContractCmts.SourceId);
    }
}
