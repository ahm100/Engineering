
namespace Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;

public class CreateProjectDocValidator : AbstractValidator<CreateProjectDocRequest>
{
    public CreateProjectDocValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(c => c.DisciplineId)
            .IsPositive(GlobalCmts.DisciplineId);

        RuleFor(c => c.DisciplineDocId)
            .IsPositive(GlobalCmts.DisciplineDocId);

        RuleFor(c => c.ThirdPartyId)
            .IsPositive(GlobalCmts.ThirdPartyId);

        RuleFor(oo => oo.Url)
            .IsRequiredString(GlobalCmts.Id);


        RuleFor(c => c.Description!)
            .IsFullString(GlobalCmts.Description, 1500, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle)
            .When(c => !string.IsNullOrWhiteSpace(c.Description));
    }
}
