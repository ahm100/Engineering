namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByContractorIds;

public record GetsProjectOperationDetailByContractorIdsResponse(
    List<GetsProjectOperationDetailByContractorIdsResponseModel> Data,
    int RowCount
    );
