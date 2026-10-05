using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduleColumn;

public record CreateProjectScheduleColumnCommand(
    long ProjectId,
    string Title,
    string? TitleEn,
    ProjectScheduleColumnDataType DataType,
    long? TargetColumnId) : ICommand<CreateProjectScheduleColumnResponse?>;