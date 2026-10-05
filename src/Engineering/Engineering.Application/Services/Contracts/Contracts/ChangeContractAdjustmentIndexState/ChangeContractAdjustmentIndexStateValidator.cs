namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentIndexState;

public class ChangeContractAdjustmentIndexStateValidator
    : AbstractValidator<ChangeContractAdjustmentIndexStateRequest>
{
    public ChangeContractAdjustmentIndexStateValidator()
    {
        RuleFor(oo => oo.ReferenceId)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);

        RuleFor(oo => oo.Id)
            .IsPositive(ContractCmts.ContractAdjustmentIndexId);
    }
}
