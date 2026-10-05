namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.UpdatePODContractorExperts;

public record UpdatePODContractorExpertsRequest(
    long Id,
    long? ConsumableVolumeExpertId,
    long? ProjectOperationDetailContractorServiceId,
    decimal? Volume,
    bool? IsActive) : IHttpRequest;