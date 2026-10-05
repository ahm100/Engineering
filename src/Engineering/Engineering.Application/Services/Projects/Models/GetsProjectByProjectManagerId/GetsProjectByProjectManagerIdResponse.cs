namespace Engineering.Application.Services.Projects.Models.GetsProjectByProjectManagerId;

public record GetsProjectByProjectManagerIdResponse(
    List<GetsProjectByProjectManagerIdModel> Data,
    int RowCount
    );
