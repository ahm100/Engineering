
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToManagementReturnedRequest(
    long Id,
    string? Description) : IHttpRequest;
