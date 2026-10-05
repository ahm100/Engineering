namespace Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;

public class UpdateBillOfLadingValidator : AbstractValidator<UpdateBillOfLadingRequest>
{
    public UpdateBillOfLadingValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(c => c.BillOfLadingName)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.BillOfLadingCode)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}