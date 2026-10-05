using Engineering.Application.Services.Machineries.Models.MachineryModels;

namespace Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;

public record GetsByMachineriesGroupIdResponse(
    List<GetsByMachineriesGroupIdModel> Data,
    int RowCount);
