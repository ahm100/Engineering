using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;

namespace Engineering.Application.Services.MachineriesGroups.Models.GetActiveMachineriesGroups;

public record GetActiveMachineriesGroupsResponse(
    List<GetsActiveMachineriesGroupModel> Data,
    int RowCount);
