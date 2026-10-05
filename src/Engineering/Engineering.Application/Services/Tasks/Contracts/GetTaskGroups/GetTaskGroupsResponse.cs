using Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;

namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroups;

public record GetTaskGroupsResponse(
    List<GetTaskGroupsModel> Data,
    int RowCount
);