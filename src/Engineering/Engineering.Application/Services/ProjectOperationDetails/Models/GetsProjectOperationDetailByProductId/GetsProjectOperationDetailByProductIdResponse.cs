using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProductId;

public record GetsProjectOperationDetailByProductIdResponse(
    List<GetsByProductIdModel> Data,
    int RowCount
    );
