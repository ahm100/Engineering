using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByExpertId;

public record GetsProjectOperationDetailByExpertIdResponse(
    List<GetsByExpertIdModel> Data,
    int RowCount
    );
