
namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;

public record GetsContractorStatusStatementDiscountByIdRequest(
    long ContractorStatusStatementId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
