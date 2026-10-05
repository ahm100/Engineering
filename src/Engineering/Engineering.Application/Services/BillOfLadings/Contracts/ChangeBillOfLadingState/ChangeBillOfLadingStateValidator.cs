namespace Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;

public class ChangeBillOfLadingStateValidator : AbstractValidator<ChangeBillOfLadingStateRequest>
{
    public ChangeBillOfLadingStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(BillOfLadingCmts.BillOfLadingId);
    }
}
