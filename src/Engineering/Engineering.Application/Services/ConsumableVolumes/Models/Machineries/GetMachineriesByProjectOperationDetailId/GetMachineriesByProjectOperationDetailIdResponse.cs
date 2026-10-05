using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationDetailId;

public record GetMachineriesByProjectOperationDetailIdResponse(
    List<ConsumableVolumeMachineryModel> Data,
    int RowCount
    );
