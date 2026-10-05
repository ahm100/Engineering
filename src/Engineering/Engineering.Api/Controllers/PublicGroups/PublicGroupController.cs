using Engineering.Application.Services.PublicGroups;
using Engineering.Application.Services.PublicGroups.Models.CreatePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.DisablePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.GetById;
using Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;
using Engineering.Application.Services.PublicGroups.Models.PublicGroupGroupDelete;

namespace Engineering.Api.Controllers.PublicGroups;

[ApiController]
[Route("api/engineering/v1/PublicGroup")]
public class PublicGroupController : ControllerBase
{
    private readonly IPublicGroupLogic _logic;

    public PublicGroupController(IPublicGroupLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreatePublicGroup")]
    [ResponseSchema<CreatePublicGroupResponse>]
    public async Task<IResult> CreatePublicGroup([FromBody] CreatePublicGroupRequest request, CT ct)
    {
        var result = await _logic.CreatePublicGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GroupDeletePublicGroup")]
    [ResponseSchema<PublicGroupGroupDeleteResponse>]
    public async Task<IResult> GroupDeletePublicGroup([FromBody] PublicGroupGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.PublicGroupGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeletePublicGroup")]
    [ResponseSchema<DisablePublicGroupResponse>]
    public async Task<IResult> DeletePublicGroup([FromBody] DisablePublicGroupRequest request, CT ct)
    {
        var result = await _logic.DisablePublicGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredPublicGroups")]
    [ResponseSchema<GetFilteredPublicGroupsResponse>]
    public async Task<IResult> GetFilteredPublicGroups([FromBody] GetFilteredPublicGroupsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredPublicGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPublicGroupById")]
    [ResponseSchema<GetPublicGroupByIdResponse>]
    public async Task<IResult> GetPublicGroupById([FromQuery] GetPublicGroupByIdRequest request, CT ct)
    {
        var result = await _logic.GetPublicGroupById(request, ct);
        return result.GetHttpResponse();
    }
}