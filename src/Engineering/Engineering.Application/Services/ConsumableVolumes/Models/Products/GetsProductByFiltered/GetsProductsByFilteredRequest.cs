namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetsProductsByFiltered;

public record GetsProductsByFilteredRequest(
    long CostCenterId,
    long? ProjectId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
