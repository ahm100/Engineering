namespace Engineering.Application.Services.OperationInfoServices.Models.GetsByOperationInfoId;

public record GetsByOperationInfoIdRequest(
    long OprationInfoId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
