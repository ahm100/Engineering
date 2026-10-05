namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsProjectOperationDetailContractors;

public record GetsProjectOperationDetailContractorsRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
