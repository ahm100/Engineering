using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsProductByOprationInfoId;

public record GetsProductByOprationInfoIdRequest(
    long OprationInfoId,
    StandardProductType? StandardProductType,
    ProductAllowedType? ProductAllowedType,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
