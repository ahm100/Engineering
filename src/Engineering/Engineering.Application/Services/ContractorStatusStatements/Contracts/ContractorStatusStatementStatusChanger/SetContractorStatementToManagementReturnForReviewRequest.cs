namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToManagementReturnForReviewRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
