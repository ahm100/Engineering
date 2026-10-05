using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleColumn;

public record EditProjectScheduleColumnRequest(
    long Id,
    string? TitleFa,
    string? TitleEn,
    ProjectScheduleColumnType? Type,
    int? SortOrder,
    ProjectScheduleColumnDataType? DataType) : IHttpRequest;