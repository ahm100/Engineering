namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToReturnToProjectManagerRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
