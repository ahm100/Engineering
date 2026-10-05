namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetFilteredProjectOperationDetailContractors;

public record GetFilteredProjectOperationDetailContractorsResponse(
    List<GetFilteredProjectOperationDetailContractorsResponseModel?> Data,
    int RowCount
    );
