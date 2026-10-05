namespace Engineering.Application.Services.Contracts.Contracts.CreateContractGuarantee;

public class CreateContractGuaranteeValidator : AbstractValidator<CreateContractGuaranteeRequest>
{
    public CreateContractGuaranteeValidator()
    {
        RuleFor(oo => oo.ContractId).IsPositive(GlobalCmts.ContractId);
        RuleFor(oo => oo.Type).IsEnum(ContractCmts.ContractGuaranteeType);
        RuleFor(oo => oo.Amount).IsPositive(ContractCmts.ContractGuaranteeAmount);
        RuleFor(oo => oo.Percentage)
            .Must(value => !value.HasValue || value.Value is > 0m and <= 100m)
            .WithError(ContractErrors.ContractGuaranteePercentageInvalid);
        RuleFor(oo => oo.Number).HasMaxLength(ContractCmts.ContractGuaranteeNumber, 250);
        RuleFor(oo => oo.IssueDate).IsDate(ContractCmts.ContractGuaranteeIssueDate);
        RuleFor(oo => oo.ExpiryDate)
            .IsDate(ContractCmts.ContractGuaranteeExpiryDate)
            .Must((request, value) => value.Date >= request.IssueDate.Date)
            .WithError(ContractErrors.ContractGuaranteeDatesInvalid);
        RuleFor(oo => oo.FileUrl)
            .MaximumLength(1500)
            .When(oo => !string.IsNullOrWhiteSpace(oo.FileUrl));
    }
}
