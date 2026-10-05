namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentReferenceState;

public class ChangeContractAdjustmentReferenceStateValidator
    : AbstractValidator<ChangeContractAdjustmentReferenceStateRequest>
{
    public ChangeContractAdjustmentReferenceStateValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ContractCmts.ContractAdjustmentReferenceId);
    }
}
