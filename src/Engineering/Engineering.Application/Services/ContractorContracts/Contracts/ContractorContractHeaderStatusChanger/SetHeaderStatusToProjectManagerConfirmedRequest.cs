
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToProjectManagerConfirmedRequest(
    long Id,
    List<string>? Urls,
    string? Description) : IHttpRequest;
