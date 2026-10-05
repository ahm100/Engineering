
namespace Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;

public record GetFilteredPublicGroupsModel(
    long Id,
    long ProductGroupId,
    string? ProductGroupName,
    string? ProductGroupCode,
    string? GroupMeasureName
    );
