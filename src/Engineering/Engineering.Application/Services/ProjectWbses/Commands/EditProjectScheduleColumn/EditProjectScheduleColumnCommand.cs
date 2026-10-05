using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleColumn;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleColumn;

public record EditProjectScheduleColumnCommand(
    long Id,
    string? TitleFa,
    string? TitleEn,
    ProjectScheduleColumnType? Type,
    int? SortOrder,
    ProjectScheduleColumnDataType? DataType) : ICommand<EditProjectScheduleColumnResponse?>;