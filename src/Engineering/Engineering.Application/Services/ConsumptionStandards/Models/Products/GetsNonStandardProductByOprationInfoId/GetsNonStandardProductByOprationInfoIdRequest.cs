namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsNonStandardProductByOprationInfoId;

public record GetsNonStandardProductByOprationInfoIdRequest(
    long OprationInfoId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
