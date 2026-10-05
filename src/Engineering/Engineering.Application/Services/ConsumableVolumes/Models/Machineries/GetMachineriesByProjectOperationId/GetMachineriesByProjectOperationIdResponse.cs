using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationId;

public record GetMachineriesByProjectOperationIdResponse(
    List<ConsumableVolumeMachineryModel> Data,
    int RowCount
    );
