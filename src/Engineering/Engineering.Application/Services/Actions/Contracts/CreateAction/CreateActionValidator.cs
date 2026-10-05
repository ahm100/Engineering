namespace Engineering.Application.Services.Actions.Contracts.CreateAction;

public class CreateActionValidator : AbstractValidator<CreateActionRequest>
{
    public CreateActionValidator()
    {
        RuleFor(c => c.Name)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
        RuleFor(c => c.Code)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);
    }
}
