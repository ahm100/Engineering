namespace Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;

public class SetECStatusValidator : AbstractValidator<SetECStatusRequest>
{
    public SetECStatusValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractErrors.UnValidId);

        RuleFor(c => c.Status)
            .IsEnum(EContractCmts.ContractStatus);
    }
}
