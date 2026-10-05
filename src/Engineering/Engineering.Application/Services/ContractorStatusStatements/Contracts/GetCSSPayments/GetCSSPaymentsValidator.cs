
namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
public class GetCSSPaymentsValidator : AbstractValidator<GetCSSPaymentsRequest>
{
    public GetCSSPaymentsValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}