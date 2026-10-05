using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;

public interface IConsumptionStandardProductRepository : IBaseRepository<ConsumptionStandardProduct>
{
    Task<ConsumptionStandardProduct?> GetById(long id, CT ct);

    Task<(List<ConsumptionStandardProduct> Data, int RowCount)> ProductGetByOprationInfoId(
        long oprationInfoId,
        StandardProductType? standardProductType,
        ProductAllowedType? productAllowedType,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ConsumptionStandardProduct> Data, int RowCount)> NonStandardProductGetByOprationInfoId(
        long operationInfoId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<ConsumptionStandardProduct> Data, int RowCount)> ProductByOprationInfoId(long categoryId, CT ct);

    Task<List<ConsumptionStandardProduct>> GetProductByOprationInfoId(
        long oprationInfoId,
        CT ct);
}