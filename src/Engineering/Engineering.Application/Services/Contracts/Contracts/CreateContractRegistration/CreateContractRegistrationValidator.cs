using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;

public class CreateContractRegistrationValidator : AbstractValidator<CreateContractRegistrationRequest>
{
    public CreateContractRegistrationValidator()
    {
        RuleFor(x => x.ContractNumber).IsPositive(ContractCmts.ContractNumber);
        RuleFor(x => x.FaTitle).HasMaxLength(GlobalCmts.FaTitle, 250);
        RuleFor(x => x.EnTitle).MaximumLength(250);
        RuleFor(x => x.Description).MaximumLength(1500);
        RuleFor(x => x.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(x => x.ContractPartyId).IsPositive(ContractCmts.ContractPartyId);
        RuleFor(x => x.StartDate).IsDate(GlobalCmts.StartDate);
        RuleFor(x => x.Duration).IsPositive(ContractCmts.Duration);
        RuleFor(x => x.DurationUnit).IsEnum(ContractCmts.DurationUnit);
        RuleFor(x => x.Status)
            .Equal(ContractStatus.Draft)
            .WithError(ContractErrors.ContractRegistrationStatusMustBeDraft);
        RuleFor(x => x.PricingMethod).IsEnum(GlobalCmts.PricingMethod);
        RuleFor(x => x.ContractTypeCode).Must(ContractRegistrationTypeCode.TryParse)
            .WithError(ContractErrors.ContractRegistrationTypeInvalid);
        RuleFor(x => x.Financial).NotNull().SetValidator(new ContractRegistrationFinancialValidator());
        RuleForEach(x => x.Urls).HasMaxLength(GlobalCmts.Url, 1500);
    }
}
