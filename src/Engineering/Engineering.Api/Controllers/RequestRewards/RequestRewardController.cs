using Engineering.Application.Services.RequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.CloseRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.ConfirmRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.DeleteRequestRewardByIds;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardStatus;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardType;
using Engineering.Application.Services.RequestRewards.Contracts.PendingRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.RejectRequestReward;

namespace Engineering.Api.Controllers.RequestRewards;

[ApiController]
[Route("api/engineering/v1/RequestReward")]
public class RequestRewardController : ControllerBase
{
    private readonly IRequestRewardLogic _logic;

    public RequestRewardController(IRequestRewardLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateRequestRewardResponse>]
    public async Task<IResult> Create([FromBody] CreateRequestRewardRequest request, CT ct)
    {
        var result = await _logic.CreateRequestReward(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFiltered")]
    [ResponseSchema<GetFilteredRequestRewardsResponse>]
    public async Task<IResult> GetFiltered([FromBody] GetFilteredRequestRewardsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestRewards(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetConfirmed")]
    [ResponseSchema<ConfirmRequestRewardResponse>]
    public async Task<IResult> SetConfirmed([FromBody] ConfirmRequestRewardRequest request, CT ct)
    {
        var result = await _logic.ConfirmRequestReward(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRejected")]
    [ResponseSchema<RejectRequestRewardResponse>]
    public async Task<IResult> SetRejected([FromBody] RejectRequestRewardRequest request, CT ct)
    {
        var result = await _logic.RejectRequestReward(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetClosed")]
    [ResponseSchema<CloseRequestRewardResponse>]
    public async Task<IResult> SetClosed([FromBody] CloseRequestRewardRequest request, CT ct)
    {
        var result = await _logic.CloseRequestReward(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetPending")]
    [ResponseSchema<PendingRequestRewardResponse>]
    public async Task<IResult> SetPending([FromBody] PendingRequestRewardRequest request, CT ct)
    {
        var result = await _logic.PendingRequestReward(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetRequestRewardByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetRequestRewardByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestRewardById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTypes")]
    [ResponseSchema<GetRequestRewardTypeResponse>]
    public async Task<IResult> GetTypes([FromQuery] GetRequestRewardTypeRequest request, CT ct)
    {
        var result = await _logic.GetRequestRewardType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetStatus")]
    [ResponseSchema<GetRequestRewardStatusResponse>]
    public async Task<IResult> GetStatus([FromQuery] GetRequestRewardStatusRequest request, CT ct)
    {
        var result = await _logic.GetRequestRewardStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredExcelExporter")]
    [ResponseSchema<GetFilteredRequestRewardsExcelExporterResponse>]
    public async Task<IResult> GetFilteredExcelExporter([FromBody] GetFilteredRequestRewardsExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestRewardsExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFilteredExcelEnums")]
    [ResponseSchema<GetFilteredRequestRewardsExcelEnumsResponse>]
    public async Task<IResult> GetFilteredExcelEnums([FromQuery] GetFilteredRequestRewardsExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestRewardsExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteRequestRewardByIds")]
    [ResponseSchema<DeleteRequestRewardByIdsResponse>]
    public async Task<IResult> DeleteRequestRewardByIds([FromBody] DeleteRequestRewardByIdsRequest request, CT ct)
    {
        var result = await _logic.DeleteRequestRewardByIds(request, ct);
        return result.GetHttpResponse();
    }
}