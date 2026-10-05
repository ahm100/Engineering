using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;

public record CreateProjectScheduleColumnRequest(
    long ProjectId,
    string TitleFa,
    string? TitleEn,
    ProjectScheduleColumnDataType DataType,
    long? TargetColumnId) : IHttpRequest;