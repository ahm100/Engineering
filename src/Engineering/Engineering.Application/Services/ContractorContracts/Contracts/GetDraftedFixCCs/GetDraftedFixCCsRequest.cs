
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;

public record GetDraftedFixCCsRequest(
    long ContractorId,
    long ProjectId,
    DateTime? StartDate,
    DateTime? EndDate
    ) : IHttpRequest;
