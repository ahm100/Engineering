namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPhysicalPercentComplete;

public class EditPhysicalPercentCompleteValidator : AbstractValidator<EditPhysicalPercentCompleteRequest>
{
    public EditPhysicalPercentCompleteValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
