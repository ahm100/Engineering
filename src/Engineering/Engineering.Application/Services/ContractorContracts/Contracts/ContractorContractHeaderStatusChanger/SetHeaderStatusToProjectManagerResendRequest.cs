
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToProjectManagerResendRequest(
    long Id,
    string? Description) : IHttpRequest;
