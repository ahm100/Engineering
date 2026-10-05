using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;

public record GetExpertsByProjectOperationIdResponse(
    List<ConsumableVolumeExpertModel> Data,
    int RowCount
    );
