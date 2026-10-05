using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

namespace Engineering.Application.Services.ProjectServices.Models.GetActiveProjectServices;

public record GetActiveProjectServicesResponse(
    List<GetsActiveProjectServiceModel> Data,
    int RowCount);
