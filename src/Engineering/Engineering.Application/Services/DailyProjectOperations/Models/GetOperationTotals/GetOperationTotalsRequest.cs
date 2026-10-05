namespace Engineering.Application.Services.DailyProjectOperations.Models.GetOperationTotals;

public record GetOperationTotalsRequest(
    long ProjectOperationId,
    long? ProjectOperationDetailId
     ) : IHttpRequest;
