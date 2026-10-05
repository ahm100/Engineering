using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Products.ProductsByOperationInfoId;

public record ProductsByOperationInfoIdQuery(
    long OprationInfoId
    ) : IQuery<DataResult<List<ConsumptionStandardProduct>>>;