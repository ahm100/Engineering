namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.CreatePODContractorExperts;

public record CreatePODContractorExpertsRequest(
    long ConsumableVolumeExpertId,
    long ProjectOperationDetailContractorServiceId,
    decimal Volume,
    bool IsActive) : IHttpRequest;