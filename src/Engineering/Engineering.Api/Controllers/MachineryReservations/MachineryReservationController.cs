using Engineering.Application.Services.MachineryReservations;
using Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.DisableMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationStatus;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationUnit;
using Engineering.Application.Services.MachineryReservations.Models.MachineryReservationGroupDelete;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservationStatus;

namespace Engineering.Api.Controllers.MachineryReservations;

[ApiController]
[Route("api/engineering/v1/MachineryReservation")]
public class MachineryReservationController : ControllerBase
{
    private readonly IMachineryReservationLogic _logic;

    public MachineryReservationController(IMachineryReservationLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateMachineryReservation")]
    [ResponseSchema<CreateMachineryReservationResponse>]
    public async Task<IResult> CreateMachineryReservation([FromBody] CreateMachineryReservationRequest request, CT ct)
    {
        var result = await _logic.CreateMachineryReservation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineryReservationGroupDelete")]
    [ResponseSchema<MachineryReservationGroupDeleteResponse>]
    public async Task<IResult> MachineryReservationGroupDelete([FromBody] MachineryReservationGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.MachineryReservationGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateMachineryReservation")]
    [ResponseSchema<UpdateMachineryReservationResponse>]
    public async Task<IResult> UpdateMachineryReservation([FromBody] UpdateMachineryReservationRequest request, CT ct)
    {
        var result = await _logic.UpdateMachineryReservation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateMachineryReservationStatus")]
    [ResponseSchema<UpdateMachineryReservationStatusResponse>]
    public async Task<IResult> UpdateMachineryReservationStatus([FromBody] UpdateMachineryReservationStatusRequest request, CT ct)
    {
        var result = await _logic.MachineryReservationStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryReservationById")]
    [ResponseSchema<GetMachineryReservationByIdResponse>]
    public async Task<IResult> GetMachineryReservationById([FromQuery] GetMachineryReservationByIdRequest request, CT ct)
    {
        var result = await _logic.GetMachineryReservationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineryReservation")]
    [ResponseSchema<GetMachineryReservationsResponse>]
    public async Task<IResult> GetsMachineryReservation([FromBody] GetMachineryReservationsRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryReservation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryReservationUnit")]
    [ResponseSchema<GetMachineryReservationUnitResponse>]
    public async Task<IResult> GetMachineryReservationUnit([FromQuery] GetMachineryReservationUnitRequest request, CT ct)
    {
        var result = await _logic.GetMachineryReservationUnit(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryReservationStatus")]
    [ResponseSchema<GetMachineryReservationStatusResponse>]
    public async Task<IResult> GetMachineryReservationStatus([FromQuery] GetMachineryReservationStatusRequest request, CT ct)
    {
        var result = await _logic.GetMachineryReservationStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableMachineryReservation")]
    [ResponseSchema<DisableMachineryReservationResponse>]
    public async Task<IResult> DisableMachineryReservation([FromBody] DisableMachineryReservationRequest request, CT ct)
    {
        var result = await _logic.DisableMachineryReservation(request, ct);
        return result.GetHttpResponse();
    }
}