using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleStartDate;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleStartDate;

public record EditProjectScheduleStartDateCommand(
    long ProjectId,
    DateTime StartDate) : ICommand<EditProjectScheduleStartDateResponse?>;