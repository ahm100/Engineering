
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToProjectManagerRejectedRequest(
    long Id,
    string? Description) : IHttpRequest;
