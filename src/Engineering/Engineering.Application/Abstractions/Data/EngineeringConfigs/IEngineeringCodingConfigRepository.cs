using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;
using Engineering.Domain.Entities.EngineeringConfig;

namespace Engineering.Application.Abstractions.Data.EngineeringConfigs;

public interface IEngineeringCodingConfigRepository : IBaseRepository<EngineeringCodingConfig>
{
    Task<(List<GetFltrCodingConfigsModel> Data, int RowCount)> GetFltrCodingConfigs(
            bool? isActive,
            int pageIndex,
            int pageSize,
            CT ct);

    Task<(List<GetCodingConfigByEngConfigModel> Data, int RowCount)> GetCodingConfigByEngConfigModel(
        long configId,
        int pageIndex,
        int pageSize, CT ct);

    Task<GetCodingConfigByIdResponse?> GetCodingConfigById(
            long id,
            CT ct);
}