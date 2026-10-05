using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.DeleteProjectOperationDetail;

public record DeleteProjectOperationDetailCommand(
    long Id
    ) : ICommand<ProjectOperationDetail>;