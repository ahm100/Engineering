using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;

namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;

public record GetMachineriesGroupsResponse(
    List<GetMachineriesGroupsWithChildModel> Data,
    int RowCount);
