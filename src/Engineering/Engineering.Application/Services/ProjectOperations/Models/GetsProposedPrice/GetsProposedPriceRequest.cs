namespace Engineering.Application.Services.ProjectOperations.Models.GetsProposedPrice;

public record GetsProposedPriceRequest(
    long OperationInfoId,
    string? FilterData,
    DateTime? StartDate,
    DateTime? EndDate,
    long? EmployerId,
    long? CostCenterId,
    long? ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
