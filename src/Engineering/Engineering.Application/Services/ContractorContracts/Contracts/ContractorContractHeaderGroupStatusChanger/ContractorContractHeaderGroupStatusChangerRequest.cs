using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderGroupStatusChanger;

public record ContractorContractHeaderGroupStatusChangerRequest(
    List<long> Ids,
    ContractorContractStatus Status,
    string? Description,
    List<string>? Urls) : IHttpRequest;
