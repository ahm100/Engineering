namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;

public record GetOperationLocationsResponse(
    List<GetOperationLocationsResponseModel> Data,
    int RowCount);
