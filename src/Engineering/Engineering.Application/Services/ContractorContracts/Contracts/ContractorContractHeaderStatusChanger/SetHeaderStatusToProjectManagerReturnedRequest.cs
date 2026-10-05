
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToProjectManagerReturnedRequest(
    long Id,
    string? Description) : IHttpRequest;
