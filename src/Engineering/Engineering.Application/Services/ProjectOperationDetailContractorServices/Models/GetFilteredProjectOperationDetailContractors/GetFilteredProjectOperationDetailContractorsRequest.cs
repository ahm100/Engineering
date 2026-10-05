namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetFilteredProjectOperationDetailContractors;

public record GetFilteredProjectOperationDetailContractorsRequest(
    List<long> CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
