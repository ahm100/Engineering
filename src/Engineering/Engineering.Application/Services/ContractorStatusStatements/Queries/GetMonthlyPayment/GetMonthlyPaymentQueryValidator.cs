namespace Engineering.Application.Services.ContractorStatusStatements.Queries.GetMonthlyPayment;

public class GetMonthlyPaymentQueryValidator : AbstractValidator<GetMonthlyPaymentQuery>
{
    public GetMonthlyPaymentQueryValidator()
    {
        RuleFor(c => c.LastMonthsCount)
            .IsPositive(GlobalCmts.Count);
    }
}