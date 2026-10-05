using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateProjectOperationStatus;

public record UpdateProjectOperationDetailStatusCommand(long ProjectOperationDetailId,
                                                        ProjectOperationDetailStatus Status,
                                                        string? StatusDescription) : ICommand<ProjectOperationDetail>;
