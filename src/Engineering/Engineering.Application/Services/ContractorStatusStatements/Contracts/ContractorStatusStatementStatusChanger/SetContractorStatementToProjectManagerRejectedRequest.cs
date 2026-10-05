namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToProjectManagerRejectedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
