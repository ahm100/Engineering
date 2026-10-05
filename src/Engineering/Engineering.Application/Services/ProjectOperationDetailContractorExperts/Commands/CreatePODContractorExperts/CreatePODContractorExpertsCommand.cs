using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreatePODContractorExperts;

public record CreatePODContractorExpertsCommand(
    long ConsumableVolumeExpertId,
    long ProjectOperationDetailContractorServiceId,
    decimal Volume,
    bool IsActive) : ICommand<ProjectOperationDetailContractorExpert?>;