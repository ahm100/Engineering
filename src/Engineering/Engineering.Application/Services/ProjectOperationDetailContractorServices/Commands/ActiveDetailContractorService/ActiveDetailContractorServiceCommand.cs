using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.ActiveDetailContractorService;

public record ActiveDetailContractorServiceCommand(
    long Id
    ) : ICommand<ProjectOperationDetailContractorService>;
