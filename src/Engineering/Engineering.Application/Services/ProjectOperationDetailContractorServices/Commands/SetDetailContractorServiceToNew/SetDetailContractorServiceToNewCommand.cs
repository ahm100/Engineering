using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetDetailContractorServiceToNew;

public record SetDetailContractorServiceToNewCommand(
    long Id
    ) : ICommand<ProjectOperationDetailContractorService>;
