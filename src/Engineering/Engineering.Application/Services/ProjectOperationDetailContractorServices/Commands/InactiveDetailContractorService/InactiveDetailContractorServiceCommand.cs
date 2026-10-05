using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.InactiveDetailContractorService;

public record InactiveDetailContractorServiceCommand(
    long Id
    ) : ICommand<ProjectOperationDetailContractorService>;
