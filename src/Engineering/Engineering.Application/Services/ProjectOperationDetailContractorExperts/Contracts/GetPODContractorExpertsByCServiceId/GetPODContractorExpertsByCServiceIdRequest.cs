namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;

public record GetPODContractorExpertsByCServiceIdRequest(
    long ProjectOperationDetailContractorServiceId,
    int PageIndex,
    int PageSize) : IHttpRequest;