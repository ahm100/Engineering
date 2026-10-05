using Engineering.Application.Services.ServiceInfos.Models.ServiceModels;

namespace Engineering.Application.Services.ServiceInfos.Models.GetActiveServices;

public record GetActiveServiceInfosResponse(
    List<GetsActiveServiceInfoModel> Data,
    int RowCount
    );