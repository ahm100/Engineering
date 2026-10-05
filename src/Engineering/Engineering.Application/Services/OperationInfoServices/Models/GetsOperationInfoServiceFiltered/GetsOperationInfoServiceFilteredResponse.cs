using Engineering.Application.Services.OperationInfoServices.Models.OperationInfoServiceModels;

namespace Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceFiltered;

public record GetsOperationInfoServiceFilteredResponse(
    List<GetsByOperationInfoIdModel> Data,
    int RowCount);
