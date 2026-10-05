using Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserModels;

namespace Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserGetsByCostCenterId;

public record InformedUserGetsByCostCenterIdResponse(
    List<InformedUserGetsByCostCenterIdModel> Data,
    int RowCount);
