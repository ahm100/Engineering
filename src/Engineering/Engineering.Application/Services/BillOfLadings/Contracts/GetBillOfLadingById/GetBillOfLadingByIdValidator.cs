namespace Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;

public class GetBillOfLadingByIdValidator : AbstractValidator<GetBillOfLadingByIdRequest>
{
    public GetBillOfLadingByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}