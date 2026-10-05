
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToManagementConfirmedRequest(
    long Id,
    List<string>? Urls,
    string? Description) : IHttpRequest;
