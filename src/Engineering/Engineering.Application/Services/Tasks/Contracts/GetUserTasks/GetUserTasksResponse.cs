namespace Engineering.Application.Services.Tasks.Contracts.GetUserTasks;
public record GetUserTasksResponse(
    List<GetUserTasksModel> Data,
    int RowCount
);