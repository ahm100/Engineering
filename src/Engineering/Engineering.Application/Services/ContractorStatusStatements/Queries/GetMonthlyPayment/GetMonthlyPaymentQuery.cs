using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;

namespace Engineering.Application.Services.ContractorStatusStatements.Queries.GetMonthlyPayment;

public record GetMonthlyPaymentQuery
(
    int LastMonthsCount
) : IQuery<GetMonthlyPaymentResponse?>;
