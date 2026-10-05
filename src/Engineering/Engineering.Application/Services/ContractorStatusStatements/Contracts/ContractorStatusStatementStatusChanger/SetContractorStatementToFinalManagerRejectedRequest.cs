namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToFinalManagerRejectedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
