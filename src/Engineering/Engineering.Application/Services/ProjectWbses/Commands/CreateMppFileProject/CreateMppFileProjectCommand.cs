using Engineering.Application.Services.ProjectWbses.Contracts.CreateMppFileProject;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateMppFileProject;

public record CreateMppFileProjectCommand(
    long ProjectId,
    DateTime? ScheduleStartDate,
    long UserId) : ICommand<CreateMppFileProjectResponse?>;