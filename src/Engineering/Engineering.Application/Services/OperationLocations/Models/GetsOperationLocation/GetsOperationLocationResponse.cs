using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;

public record GetsOperationLocationResponse(
    List<GetOperationLocationsWithChildModel> Data,
    int RowCount);
