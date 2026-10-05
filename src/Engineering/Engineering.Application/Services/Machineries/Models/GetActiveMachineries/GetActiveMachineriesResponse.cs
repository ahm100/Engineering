using Engineering.Application.Services.Machineries.Models.MachineryModels;

namespace Engineering.Application.Services.Machineries.Models.GetActiveMachineries;

public record GetActiveMachineriesResponse(
    List<GetsActiveMachineryModel> Data,
    int RowCount
    );

