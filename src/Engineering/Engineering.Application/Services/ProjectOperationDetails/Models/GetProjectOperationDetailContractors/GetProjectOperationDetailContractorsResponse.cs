namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailContractors;

public record GetProjectOperationDetailContractorsResponse(
    List<GetProjectOperationDetailContractorsResponseModel> Data,
    int RowCount
    );
