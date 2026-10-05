namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToPrimaryManagerConfirmedRequest(
    long Id,
    string? Description,
    decimal? ConfirmedAmount
    ) : IHttpRequest;
