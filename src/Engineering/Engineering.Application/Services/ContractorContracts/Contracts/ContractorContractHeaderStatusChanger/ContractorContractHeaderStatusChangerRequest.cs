using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record ContractorContractHeaderStatusChangerRequest(
    long Id,
    ContractorContractStatus Status,
    string? Description,
    List<string>? Urls
    ) : IHttpRequest;

public record ContractorContractHeaderStatusChangerModelRequest(
    long? Id,
    ContractorContractHeader? ContractorContractHeader,
    ContractorContractStatus Status,
    string? Description,
    List<string>? Urls
    ) : IHttpRequest;
