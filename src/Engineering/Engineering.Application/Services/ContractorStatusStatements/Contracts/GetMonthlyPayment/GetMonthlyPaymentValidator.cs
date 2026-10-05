namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;

public class GetMonthlyPaymentValidator : AbstractValidator<GetMonthlyPaymentRequest>
{
    public GetMonthlyPaymentValidator()
    {
        RuleFor(c => c.LastMonthsCount)
            .IsPositive(GlobalCmts.Count);
    }
}