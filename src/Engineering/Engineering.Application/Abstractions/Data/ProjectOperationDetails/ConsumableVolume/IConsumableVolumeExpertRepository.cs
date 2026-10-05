using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;

public interface IConsumableVolumeExpertRepository : IBaseRepository<ConsumableVolumeExpert>
{
    Task<ConsumableVolumeExpert?> GetById(long id, CT ct);

    Task<(List<ConsumableVolumeExpert> Data, int RowCount)> GetsByProjectOperationDetailId(long id, CT ct);
    Task<(List<ExpertsDataModel> Data, int RowCount)> GetsByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct);
    Task<List<ConsumableVolumeExpert>> GetExpertsByProjectOperationIds(List<long> projectOperationIds, CT ct);

}