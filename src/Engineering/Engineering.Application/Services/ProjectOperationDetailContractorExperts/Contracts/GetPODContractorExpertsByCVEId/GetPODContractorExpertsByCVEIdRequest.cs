namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;

public record GetPODContractorExpertsByCVEIdRequest(
    long ConsumableVolumeExpertId,
    int PageIndex,
    int PageSize) : IHttpRequest;