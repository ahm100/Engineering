using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.StateChangerDetailContractorServices;

public record StateChangerDetailContractorServicesCommand(
    List<ProjectOperationDetailContractorService> Items,
    bool State
    ) : ICommand<bool?>;
