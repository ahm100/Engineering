namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToProjectManagerReturnedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
