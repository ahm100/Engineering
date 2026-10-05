
namespace Engineering.Application.Services.Projects.Models.GetsProjectByIds;

public record GetsProjectByIdsResponse(
    List<GetsProjectByIdsResponseModel> Data,
    int RowCount);
