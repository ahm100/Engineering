using Engineering.Domain.Entities.OperationInfos.Enums;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;


namespace Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsProductByOperationInfoId;

public record GetsProductByOperationInfoIdQuery(
    long OprationInfoId,
    StandardProductType? StandardProductType,
    ProductAllowedType? ProductAllowedType,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumptionStandardProduct>>>;