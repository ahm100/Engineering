namespace Engineering.Application.Services.Projects.Models.GetsByEmployerId;

public record GetsProjectByEmployerIdResponse(
    List<GetsProjectByEmployerIdModel> Data,
    int RowCount
    );
