namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailContractors;

public record GetProjectOperationDetailContractorsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
