using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetailDate;

public record UpdateProjectOperationDetailDateCommand(long ProjectOperationDetailId,
                                                      DateTime StartDate,
                                                      DateTime EndDate) : ICommand<ProjectOperationDetail>;
