
namespace Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;

public record SetHeaderStatusToManagementReturnForReviewRequest(
    long Id,
    string? Description) : IHttpRequest;
