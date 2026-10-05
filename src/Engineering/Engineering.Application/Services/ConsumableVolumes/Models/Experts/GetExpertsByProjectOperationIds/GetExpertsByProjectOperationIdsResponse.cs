using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationIds;

public record GetExpertsByProjectOperationIdsResponse(
    List<GetExpertsByProjectOperationIdsModel> Data,
    int RowCount
    );
