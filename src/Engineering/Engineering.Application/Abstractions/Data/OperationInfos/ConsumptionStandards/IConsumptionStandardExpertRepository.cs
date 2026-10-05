using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;

public interface IConsumptionStandardExpertRepository : IBaseRepository<ConsumptionStandardExpert>
{
    Task<ConsumptionStandardExpert?> GetById(long id, CT ct);
    Task<(List<ConsumptionStandardExpert> Data, int RowCount)> ExpertGetByOprationInfoId(long categoryId, int pageIndex, int pageSize, CT ct);
    Task<(List<ConsumptionStandardExpert> Data, int RowCount)> ExpertsByOprationInfoId(long categoryId, CT ct);
}