namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToManagementRejectedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
