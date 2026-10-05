namespace Engineering.Application.Services.Contracts.Contracts.ChangeContractGuaranteeStatus;

public class ChangeContractGuaranteeStatusValidator : AbstractValidator<ChangeContractGuaranteeStatusRequest>
{
    public ChangeContractGuaranteeStatusValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Status).IsEnum(ContractCmts.ContractGuaranteeStatus);
    }
}
