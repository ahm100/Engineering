using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;

namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;

public class UpdateEContractHeaderValidator : AbstractValidator<UpdateEContractHeaderRequest>
{
    public UpdateEContractHeaderValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.EmployerContractHead);

        When(oo => !string.IsNullOrEmpty(oo.Code), () =>
        {
            RuleFor(c => c.Code)
                .IsFullString(GlobalCmts.Code, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });

        RuleFor(c => c.Type)
            .IsEnum(EContractCmts.CurrencyId);

        When(oo => oo.CreateContracts != null && oo.CreateContracts.Any(), () =>
        {
            RuleForEach(oo => oo.CreateContracts)
                .NotEmpty().SetValidator(new CreateEContractModelValidator());
        });

        When(oo => oo.UpdateContracts != null && oo.UpdateContracts.Any(), () =>
        {
            RuleForEach(oo => oo.UpdateContracts)
                .NotEmpty().SetValidator(new UpdateEContractValidator());
        });

        When(oo => !string.IsNullOrEmpty(oo.Description), () =>
        {
            RuleFor(c => c.Description)
                .IsFullString(GlobalCmts.Code, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        });
    }
}
