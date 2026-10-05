namespace Engineering.Application.Services.Actions.Contracts.UpdateAction;

public class UpdateActionValidator : AbstractValidator<UpdateActionRequest>
{
    public UpdateActionValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(c => c.Name)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Code)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}