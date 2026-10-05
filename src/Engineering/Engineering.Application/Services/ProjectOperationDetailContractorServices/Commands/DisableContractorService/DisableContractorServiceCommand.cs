using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.DisableContractorService;

public record DisableContractorServiceCommand(
    long Id,
    long? projectOperationDetailId
    ) : ICommand<ProjectOperationDetailContractorService>;