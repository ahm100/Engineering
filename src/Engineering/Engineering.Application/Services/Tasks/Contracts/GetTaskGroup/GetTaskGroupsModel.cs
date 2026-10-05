namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;

public record GetTaskGroupsModel(
    long Id,
    string Title,
    string? Description
);