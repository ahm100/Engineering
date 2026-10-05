

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;

public record GetContractorContractHistoryRequest(
    long ContractorContractId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
