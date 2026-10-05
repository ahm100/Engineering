using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;


namespace Engineering.Application.Services.ConsumptionStandards.Queries.Products.GetsNonStandardProductByOprationInfoId;

public record GetsNonStandardProductByOprationInfoIdQuery(
    long OprationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumptionStandardProduct>>>;