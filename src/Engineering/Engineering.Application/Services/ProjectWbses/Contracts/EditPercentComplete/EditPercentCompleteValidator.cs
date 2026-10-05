namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPercentComplete;

public class EditPercentCompleteValidator : AbstractValidator<EditPercentCompleteRequest>
{
    public EditPercentCompleteValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
