namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;

public record GetMonthlyPaymentRequest(
    int LastMonthsCount) : IHttpRequest;