using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

namespace Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;

public record GetsOperationLocationByCostCenterIdResponse(
    List<GetsOperationLocationByCostCenterIdModel> Data,
    int RowCount
    );
