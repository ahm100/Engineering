using Engineering.Application.Services.PublicGroups.Models.CreatePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.DisablePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.GetById;
using Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;
using Engineering.Application.Services.PublicGroups.Models.PublicGroupGroupDelete;

namespace Engineering.Application.Services.PublicGroups;

public interface IPublicGroupLogic
{
    Task<Result<CreatePublicGroupResponse?>> CreatePublicGroup(CreatePublicGroupRequest request, CT ct);
    Task<Result<DisablePublicGroupResponse?>> DisablePublicGroup(DisablePublicGroupRequest request, CT ct);
    Task<Result<PublicGroupGroupDeleteResponse?>> PublicGroupGroupDelete(PublicGroupGroupDeleteRequest request, CT ct);
    Task<Result<GetPublicGroupByIdResponse?>> GetPublicGroupById(GetPublicGroupByIdRequest request, CT ct);
    Task<Result<GetFilteredPublicGroupsResponse?>> GetFilteredPublicGroups(GetFilteredPublicGroupsRequest request, CT ct);
}