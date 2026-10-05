using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeModels;

namespace Engineering.Application.Services.ProjectTypes.Models.GetActiveProjectTypes;

public record GetActiveProjectTypesResponse(
    List<GetsActiveProjectTypeModel> Data,
    int RowCount);
