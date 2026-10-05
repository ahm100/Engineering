using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Application.Abstractions.Data.EngineeringConfigs;

public interface IEngineeringConfigHistoryRepository : IBaseRepository<EngineeringConfigHistory>
{
    Task<EngineeringConfigHistory?> GetConfigById(
        long id, CT ct);

    Task<EngineeringConfigHistory?> GetByConfigId(
        long id, CT ct);

    Task<(List<GetConfigHistoryByConfigIdModel>? Data, int Count)> GetConfigHistoryByConfigId(
        long id,
        int pageIndex,
        int pageSize, CT ct);
}