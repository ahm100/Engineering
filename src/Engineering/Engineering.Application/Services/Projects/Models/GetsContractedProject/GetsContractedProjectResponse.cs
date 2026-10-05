
namespace Engineering.Application.Services.Projects.Models.GetsContractedProject;

public record GetsContractedProjectResponse(
    List<GetsContractedProjectResponseModel> Data,
    int RowCount
    );
