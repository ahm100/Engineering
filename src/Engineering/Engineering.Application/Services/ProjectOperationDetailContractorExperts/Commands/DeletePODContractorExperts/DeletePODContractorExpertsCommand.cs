using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.DeletePODContractorExperts;

public record DeletePODContractorExpertsCommand(
    long Id) : ICommand<ProjectOperationDetailContractorExpert?>;