using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetsByProjectOperationDetailId;

public record GetExpertsByProjectOperationDetailIdResponse(
    List<ConsumableVolumeExpertModel> Data,
    int RowCount
    );
