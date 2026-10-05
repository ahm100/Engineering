
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;

namespace Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;

public class CreateEContractHeaderValidator : AbstractValidator<CreateEContractHeaderRequest>
{
    public CreateEContractHeaderValidator()
    {
        RuleFor(c => c.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);

        When(oo => !string.IsNullOrEmpty(oo.Code), () =>
        {
            RuleFor(c => c.Code)
                .IsFullString(GlobalCmts.Code, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

        RuleFor(c => c.CurrencyId)
            .IsPositive(GlobalCmts.CurrencyId);

        RuleFor(c => c.Type)
            .IsEnum(EContractCmts.EContractType);

        RuleForEach(oo => oo.CreateContracts)
            .NotEmpty().SetValidator(new CreateEContractModelValidator());

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Code, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });
    }
}
