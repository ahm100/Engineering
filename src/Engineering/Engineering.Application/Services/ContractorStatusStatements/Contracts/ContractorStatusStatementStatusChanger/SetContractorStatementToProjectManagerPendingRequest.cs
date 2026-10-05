namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToProjectManagerPendingRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
