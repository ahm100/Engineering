namespace Engineering.Application.Services.ContractorStatusStatements.Queries.GetTodaysPayment;

public record GetTodaysPaymentQuery(
    ) : IQuery<decimal?>;
