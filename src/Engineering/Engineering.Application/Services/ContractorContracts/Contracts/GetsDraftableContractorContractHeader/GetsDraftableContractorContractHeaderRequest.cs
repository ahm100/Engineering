
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;

public record GetsDraftableContractorContractHeaderRequest(
    long ContractorId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
