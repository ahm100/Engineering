
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToManagementRejectedRequest(
    long Id,
    string? Description) : IHttpRequest;
