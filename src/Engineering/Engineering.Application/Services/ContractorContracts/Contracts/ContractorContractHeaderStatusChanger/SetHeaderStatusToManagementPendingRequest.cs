
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToManagementPendingRequest(
    long Id,
    string? Description) : IHttpRequest;
