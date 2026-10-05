using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

namespace Engineering.Application.Services.OperationLocations.Models.GetActiveOperationLocations;

public record GetActiveOperationLocationsResponse(
    List<GetsActiveOperationLocationModel> Data,
    int RowCount
    );

