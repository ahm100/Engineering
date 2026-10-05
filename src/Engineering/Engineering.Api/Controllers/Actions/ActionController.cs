using Engineering.Application.Services.Actions;
using Engineering.Application.Services.Actions.Contracts.ActionCodeCreator;
using Engineering.Application.Services.Actions.Contracts.CreateAction;
using Engineering.Application.Services.Actions.Contracts.DeleteActions;
using Engineering.Application.Services.Actions.Contracts.GetActionById;
using Engineering.Application.Services.Actions.Contracts.GetFilteredActions;
using Engineering.Application.Services.Actions.Contracts.GetsActiveActions;
using Engineering.Application.Services.Actions.Contracts.UpdateAction;

[Authorize]
[Route("api/engineering/v1/Action")]
public class ActionController : ControllerBase
{
    private readonly ILogger<ActionController> _logger;
    private readonly IActionLogic _logic;

    public ActionController(
        ILogger<ActionController> logger,
        IActionLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateAction")]
    [ResponseSchema<CreateActionResponse>]
    public async Task<IResult> CreateAction(
        [FromBody] CreateActionRequest request, CT ct)
    {
        _logger.LogInformation("CreateAction");
        var result = await _logic.CreateAction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAction")]
    [ResponseSchema<DeleteActionResponse>]
    public async Task<IResult> DeleteAction(
        [FromQuery] DeleteActionRequest request, CT ct)
    {
        _logger.LogInformation("DeleteAction");
        var result = await _logic.DeleteAction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateAction")]
    [ResponseSchema<UpdateActionResponse>]
    public async Task<IResult> UpdateAction(
        [FromBody] UpdateActionRequest request, CT ct)
    {
        _logger.LogInformation("UpdateAction");
        var result = await _logic.UpdateAction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetActionById")]
    [ResponseSchema<GetActionByIdResponse>]
    public async Task<IResult> GetActionById(
        [FromQuery] GetActionByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetActionById");
        var result = await _logic.GetActionById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveActions")]
    [ResponseSchema<GetsActiveActionsResponse>]
    public async Task<IResult> GetsActiveActions(
        [FromBody] GetsActiveActionsRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveActions");
        var result = await _logic.GetsActiveActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredActions")]
    [ResponseSchema<GetFilteredActionsResponse>]
    public async Task<IResult> GetFilteredActions(
        [FromBody] GetFilteredActionsRequest request, CT ct)
    {
        _logger.LogInformation("GetFilteredActions");
        var result = await _logic.GetFilteredActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("ActionCodeCreator")]
    [ResponseSchema<ActionCodeCreatorResponse>]
    public async Task<IResult> ActionCodeCreator(
        [FromQuery] ActionCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("ActionCodeCreator");
        var result = await _logic.ActionCodeCreator(request, ct);
        return result.GetHttpResponse();
    }
}