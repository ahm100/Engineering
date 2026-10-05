namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;

public record GetCSSPaymentsRequest(
    long Id,
    int PageIndex,
    int PageSize
) : IHttpRequest;