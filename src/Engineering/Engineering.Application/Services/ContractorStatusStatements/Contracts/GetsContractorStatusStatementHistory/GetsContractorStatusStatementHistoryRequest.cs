
namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;

public record GetsContractorStatusStatementHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
