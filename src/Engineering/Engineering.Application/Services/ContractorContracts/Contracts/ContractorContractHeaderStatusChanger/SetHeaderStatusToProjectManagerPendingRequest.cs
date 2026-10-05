
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToProjectManagerPendingRequest(
    long Id,
    string? Description) : IHttpRequest;
