using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

namespace Engineering.Application.Services.OperationLocations.Models.GetsWithoutParentOperationLocation;

public record GetsWithoutParentOperationLocationResponse(
    List<GetsOperationLocationByCostCenterIdModel> Data,
    int RowCount
    );
