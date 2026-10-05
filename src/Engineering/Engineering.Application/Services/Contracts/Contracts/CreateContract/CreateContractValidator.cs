namespace Engineering.Application.Services.Contracts.Contracts.CreateContract;

public class CreateContractValidator : AbstractValidator<CreateContractRequest>
{
    public CreateContractValidator()
    {
        RuleFor(oo => oo.FaTitle)
            .HasMaxLength(GlobalCmts.FaTitle, 250);

        RuleFor(oo => oo.EnTitle)
            .MaximumLength(250);

        RuleFor(oo => oo.Description)
            .MaximumLength(1500);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.ContractPartyId)
            .IsPositive(ContractCmts.ContractPartyId);

        RuleFor(oo => oo.StartDate)
            .IsDate(GlobalCmts.StartDate);

        RuleFor(oo => oo.Duration)
            .IsPositive(ContractCmts.Duration);

        RuleFor(oo => oo.DurationUnit)
            .IsEnum(ContractCmts.DurationUnit);

        RuleForEach(oo => oo.Urls)
            .IsRequiredString(GlobalCmts.Url)
            .MaximumLength(1500)
            .WithError(GlobalErrors.RequiredMaxLength(GlobalCmts.Url, 1500));
    }
}
