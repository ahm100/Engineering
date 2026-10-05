namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToFinalManagerConfirmedRequest(
    long Id,
    string? Description,
    decimal? ConfirmedAmount
    ) : IHttpRequest;
