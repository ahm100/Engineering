
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;

public record GetFilteredContractorContractHeaderInfoRequest(
    long ContractorContractTypeId,
    List<long> ProjectOperationDetailServiceIds,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
