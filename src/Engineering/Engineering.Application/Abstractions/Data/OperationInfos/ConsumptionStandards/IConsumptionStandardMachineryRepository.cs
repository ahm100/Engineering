using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

namespace Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;

public interface IConsumptionStandardMachineryRepository : IBaseRepository<ConsumptionStandardMachinery>
{
    Task<ConsumptionStandardMachinery?> GetById(long id, CT ct);
    Task<(List<ConsumptionStandardMachinery> Data, int RowCount)> MachineryGetByOprationInfoId(long categoryId, int pageIndex, int pageSize, CT ct);
    Task<(List<ConsumptionStandardMachinery> Data, int RowCount)> MachineriesByOprationInfoId(long categoryId, CT ct);
}