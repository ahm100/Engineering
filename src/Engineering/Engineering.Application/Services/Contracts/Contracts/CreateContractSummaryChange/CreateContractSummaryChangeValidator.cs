using Engineering.Domain.Entities.Contracts;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;

public class CreateContractSummaryChangeValidator : AbstractValidator<CreateContractSummaryChangeRequest>
{
    public CreateContractSummaryChangeValidator()
    {
        RuleFor(x => x.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(x => x.Type).IsEnum(ContractCmts.ContractChangeType);
        RuleFor(x => x.Number).HasMaxLength(ContractCmts.ContractChangeNumber, 250);
        RuleFor(x => x.Date).IsDate(GlobalCmts.Date);
        RuleFor(x => x.Subject).HasMaxLength(ContractCmts.ContractChangeSubject, 250);
        RuleFor(x => x.FinancialChangeAmount)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.NewContractAmount)
            .PrecisionScale(ContractFinancialMath.MoneyPrecision, ContractFinancialMath.MoneyScale, true);
        RuleFor(x => x.DurationChange).Must(x => !x.HasValue || x != 0)
            .WithError(ContractErrors.ContractChangeDurationInvalid);
        RuleForEach(x => x.Urls).HasMaxLength(GlobalCmts.Url, 1500);
    }
}
