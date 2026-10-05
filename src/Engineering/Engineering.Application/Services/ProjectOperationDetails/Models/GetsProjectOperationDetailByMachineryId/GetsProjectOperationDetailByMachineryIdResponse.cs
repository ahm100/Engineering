using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByMachineryId;

public record GetsProjectOperationDetailByMachineryIdResponse(
    List<GetsByMachineryIdModel> Data,
    int RowCount
    );
