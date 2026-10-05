namespace Engineering.Application.Services.BillOfLadings.Contracts.CreateBillOfLading;

public class CreateBillOfLadingValidator : AbstractValidator<CreateBillOfLadingRequest>
{
    public CreateBillOfLadingValidator()
    {
        RuleFor(c => c.BillOfLadingName)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.BillOfLadingCode)
            .IsFullString(GlobalCmts.Description, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

    }
}