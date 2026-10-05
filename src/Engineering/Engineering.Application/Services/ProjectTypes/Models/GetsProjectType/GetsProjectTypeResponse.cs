using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeModels;

namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectType;

public record GetsProjectTypeResponse(
    List<GetsProjectTypeModel> Data,
    int RowCount
    );
