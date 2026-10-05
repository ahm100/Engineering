namespace Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;

public record CreateOperationInfoServiceRequestModel(
    long Id,
    string TimeSpant,
    bool IsDeleted
     ) : IHttpRequest;
