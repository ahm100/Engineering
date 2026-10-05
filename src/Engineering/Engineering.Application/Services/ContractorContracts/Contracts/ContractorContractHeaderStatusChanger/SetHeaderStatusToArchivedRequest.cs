
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToArchivedRequest(
    long Id,
    string? Description) : IHttpRequest;
