
namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;

public record GetContractorStatusStatementByIdRequest(
    long Id
    ) : IHttpRequest;
