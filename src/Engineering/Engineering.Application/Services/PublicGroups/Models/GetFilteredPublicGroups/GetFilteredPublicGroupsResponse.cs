namespace Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;

public record GetFilteredPublicGroupsResponse(
    List<GetFilteredPublicGroupsModel>? Data,
    int RowCount
    );
