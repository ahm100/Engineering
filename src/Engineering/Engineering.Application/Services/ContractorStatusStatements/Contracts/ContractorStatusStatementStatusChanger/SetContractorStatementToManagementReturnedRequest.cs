namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToManagementReturnedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
