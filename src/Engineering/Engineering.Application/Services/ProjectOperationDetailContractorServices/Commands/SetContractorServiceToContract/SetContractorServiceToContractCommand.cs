using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.SetContractorServiceToContract;

public record SetContractorServiceToContractCommand(
    long Id
    ) : ICommand<ProjectOperationDetailContractorService>;
