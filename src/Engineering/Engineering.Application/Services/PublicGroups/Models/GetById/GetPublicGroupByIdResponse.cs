
namespace Engineering.Application.Services.PublicGroups.Models.GetById;

public record GetPublicGroupByIdResponse(
    long Id,
    long ProductGroupId,
    string? ProductGroupName,
    string? ProductGroupCode,
    string? GroupMeasureName
    );
