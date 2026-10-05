using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Application.Abstractions.Data.EngineeringConfigs;

public interface IEngineeringConfigRepository : IBaseRepository<EngineeringConfig>
{
    Task<List<EngineeringConfig>> GetActiveEngineeringConfigs(
            long companyId,
            CT ct);

    Task<EngineeringConfig?> GetConfigById(
        long id, CT ct);

    Task<EngineeringConfig?> GetActiveConfig(
        long companyId, CT ct);

    Task<(List<GetFltrConfigsModel> Data, int RowCount)> GetFltrConfigs(
            long companyId,
            bool? sendTelegramMessage,
            bool? isActive,
            int pageIndex,
            int pageSize,
            CT ct);
}
