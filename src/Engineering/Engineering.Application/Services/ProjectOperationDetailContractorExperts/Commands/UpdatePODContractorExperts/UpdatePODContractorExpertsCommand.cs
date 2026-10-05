using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdatePODContractorExperts;

public record UpdatePODContractorExpertsCommand(
    long Id,
    long? ConsumableVolumeExpertId,
    long? ProjectOperationDetailContractorServiceId,
    decimal? Volume,
    bool? IsActive) : ICommand<ProjectOperationDetailContractorExpert?>;