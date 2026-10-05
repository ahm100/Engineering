using Engineering.Application.Services.Advertisements;
using Engineering.Application.Services.Advertisements.Commands.SetAdsDetails;
using Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;
using Engineering.Application.Services.Advertisements.Contracts.CreateAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.SetAdsDetails;
using Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;
using MediatR;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Advertisements;

[Authorize]
[Route("api/engineering/v1/Advertisement")]
public class AdvertisementController : ControllerBase
{
    private readonly ILogger<AdvertisementController> _logger;
    private readonly IMediator _mediator;
    private readonly IAdvertisementLogic _logic;

    public AdvertisementController(
        ILogger<AdvertisementController> logger,
        IMediator mediator,
        IAdvertisementLogic logic) : base()
    {
        _logger = logger;
        _mediator = mediator;
        _logic = logic;
    }

    [HttpPost("CreateAdvertisement")]
    [ResponseSchema<CreateAdvertisementResponse>]
    public async Task<IResult> CreateAdvertisement(
        [FromBody] CreateAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("CreateAdvertisement");
        var result = await _logic.CreateAdvertisement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateAdvertisement")]
    [ResponseSchema<UpdateAdvertisementResponse>]
    public async Task<IResult> UpdateAdvertisement(
        [FromBody] UpdateAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("UpdateAdvertisement");
        var result = await _logic.UpdateAdvertisement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetAdsDetails")]
    [ResponseSchema<SetAdsDetailsResponse>]
    public async Task<IResult> SetAdsDetails(
        [FromBody] SetAdsDetailsRequest request, CT ct)
    {
        var result = await _mediator.Send(
            new SetAdsDetailsCommand(
                request.Id,
                request.AdEnName,
                request.DescriptionFa,
                request.DescriptionEn),
            ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAdvertisement")]
    [ResponseSchema<DeleteAdvertisementResponse>]
    public async Task<IResult> DeleteAdvertisement(
        [FromQuery] DeleteAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("DeleteAdvertisement");
        var result = await _logic.DeleteAdvertisement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveAdvertisement")]
    [Description("فعال کردن تبلیغ")]
    [ResponseSchema<ChangeAdvertisementStateResponse>]
    public async Task<IResult> ActivePickerMan([FromBody] ActiveAdvertisementRequest request, CT ct)
    {
        var result = await _logic.ChangeAdvertisementState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InActiveAdvertisement")]
    [Description("غیرفعال کردن تبلیغ")]
    [ResponseSchema<ChangeAdvertisementStateResponse>]
    public async Task<IResult> InActivePickerMan([FromBody] InActiveAdvertisementRequest request, CT ct)
    {
        var result = await _logic.ChangeAdvertisementState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeAdvertisementState")]
    [ResponseSchema<ChangeAdvertisementStateResponse>]
    public async Task<IResult> ChangeAdvertisementState(
        [FromBody] ChangeAdvertisementStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeAdvertisementState");
        var result = await _logic.ChangeAdvertisementState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetAdvertisementById")]
    [ResponseSchema<GetAdvertisementByIdResponse>]
    public async Task<IResult> GetAdvertisementById(
        [FromQuery] GetAdvertisementByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetAdvertisementById");
        var result = await _logic.GetAdvertisementById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrAdvertisement")]
    [ResponseSchema<GetFltrAdvertisementResponse>]
    public async Task<IResult> GetFltrAdvertisement(
        [FromBody] GetFltrAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrAdvertisement");
        var result = await _logic.GetFltrAdvertisement(request, ct);
        return result.GetHttpResponse();
    }
}