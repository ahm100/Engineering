using Engineering.Application.Services.Machineries.Models.MachineryModels;

namespace Engineering.Application.Services.Machineries.Models.GetMachineries;

public record GetMachineriesResponse(
    List<GetMachineriesModel> Data,
    int RowCount);
