
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorByContractorContractType;

public record GetsContractorByContractorContractTypeRequest(
    long ContractorContractTypeId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
