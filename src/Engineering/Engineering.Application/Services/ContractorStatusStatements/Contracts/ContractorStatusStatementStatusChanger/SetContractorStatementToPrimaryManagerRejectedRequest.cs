namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToPrimaryManagerRejectedRequest(
    long Id,
    string? Description
    ) : IHttpRequest;
