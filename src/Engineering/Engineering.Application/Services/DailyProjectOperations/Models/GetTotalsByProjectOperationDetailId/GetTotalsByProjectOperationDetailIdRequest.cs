namespace Engineering.Application.Services.DailyProjectOperations.Models.GetTotalsByProjectOperationDetailId;

public record GetTotalsByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId
     ) : IHttpRequest;
