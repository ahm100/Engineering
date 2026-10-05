
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;

public record GetDraftedServiceCCsRequest(
    long ContractorId,
    long ProjectId,
    DateTime StartDate,
    DateTime EndDate
    ) : IHttpRequest;
