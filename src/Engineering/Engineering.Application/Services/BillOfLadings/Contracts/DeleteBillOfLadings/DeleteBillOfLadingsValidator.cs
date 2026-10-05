namespace Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;

public class DeleteBillOfLadingsValidator : AbstractValidator<DeleteBillOfLadingsRequest>
{
    public DeleteBillOfLadingsValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(BillOfLadingCmts.BillOfLadingId);
    }
}