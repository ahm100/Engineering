
namespace Engineering.Application.Services.ContractorStatusStatements.Models.DeleteContractorStatusStatement;

public record DeleteContractorStatusStatementRequest(
    long Id
    ) : IHttpRequest;
