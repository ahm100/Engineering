namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailStatus;

public record GetsProjectOperationDetailStatusRequest(
    bool? RemoveNotStarted
     ) : IHttpRequest;
