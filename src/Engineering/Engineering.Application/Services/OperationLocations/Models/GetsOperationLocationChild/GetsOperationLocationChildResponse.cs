using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;

public record GetsOperationLocationChildResponse(
    List<GetOperationLocationsWithChildModel> Data,
    int RowCount
    );
