namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToManagementPendingRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
