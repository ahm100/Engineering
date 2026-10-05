using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdatesProjectOperationDetailDate;

public record UpdatesProjectOperationDetailDateCommand(
    ProjectOperationDetail ProjectOperationDetail,
    DateTime? StartDate,
    DateTime? EndDate
    ) : ICommand<ProjectOperationDetail>;