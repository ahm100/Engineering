using Engineering.Application.Services.ServiceInfos.Models.ServiceModels;

namespace Engineering.Application.Services.ServiceInfos.Models.GetServices;

public record GetServiceInfosResponse(
    List<GetServiceInfosModel> Data,
    int RowCount);
