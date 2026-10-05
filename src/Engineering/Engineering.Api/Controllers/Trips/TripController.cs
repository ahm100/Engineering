using Engineering.Application.Services.Trips;
using Engineering.Application.Services.Trips.Models.Active;
using Engineering.Application.Services.Trips.Models.CodeCreator;
using Engineering.Application.Services.Trips.Models.Create;
using Engineering.Application.Services.Trips.Models.Disable;
using Engineering.Application.Services.Trips.Models.GetByCode;
using Engineering.Application.Services.Trips.Models.GetById;
using Engineering.Application.Services.Trips.Models.GetByName;
using Engineering.Application.Services.Trips.Models.GetsActive;
using Engineering.Application.Services.Trips.Models.GetsFiltered;
using Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;
using Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;
using Engineering.Application.Services.Trips.Models.Inactive;
using Engineering.Application.Services.Trips.Models.StateChangerTrips;
using Engineering.Application.Services.Trips.Models.TripExcelImports;
using Engineering.Application.Services.Trips.Models.TripGroupDelete;
using Engineering.Application.Services.Trips.Models.Update;

namespace Engineering.Api.Controllers.Trips;

[ApiController]
[Route("api/engineering/v1/Trip")]
public class TripController : ControllerBase
{
    private readonly ITripLogic _logic;

    public TripController(ITripLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTrip")]
    [ResponseSchema<CreateTripResponse>]
    public async Task<IResult> AddTrip(
    [FromBody] CreateTripRequest request,
    CT ct)
    {
        var result = await _logic.CreateTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TripExcelImports")]
    [ResponseSchema<TripExcelImportsResponse>]
    public async Task<IResult> TripExcelImports(
        [FromBody] TripExcelImportsRequest request,
        CT ct)
    {
        var result = await _logic.TripExcelImports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TripCodeCreator")]
    [ResponseSchema<TripCodeCreatorResponse>]
    public async Task<IResult> TripCodeCreator(
        [FromBody] TripCodeCreatorRequest request,
        CT ct)
    {
        var result = await _logic.TripCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TripGroupDelete")]
    [ResponseSchema<TripGroupDeleteResponse>]
    public async Task<IResult> TripGroupDelete(
        [FromBody] TripGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.TripGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateTrips")]
    [ResponseSchema<StateChangerTripsResponse>]
    public async Task<IResult> ActivateTrips(
        [FromBody] ActivateTripsRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerTrips(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateTrips")]
    [ResponseSchema<StateChangerTripsResponse>]
    public async Task<IResult> InactivateTrips(
        [FromBody] InactivateTripsRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerTrips(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTrip")]
    [ResponseSchema<UpdateTripResponse>]
    public async Task<IResult> EditTrip(
        [FromBody] UpdateTripRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTrip")]
    [ResponseSchema<ActiveTripResponse>]
    public async Task<IResult> ActiveTrip(
        [FromBody] ActiveTripRequest request,
        CT ct)
    {
        var result = await _logic.ActiveTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTrip")]
    [ResponseSchema<InactiveTripResponse>]
    public async Task<IResult> InactiveTrip(
        [FromBody] InactiveTripRequest request,
        CT ct)
    {
        var result = await _logic.InactiveTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTripById")]
    [ResponseSchema<GetTripByIdResponse>]
    public async Task<IResult> GetTripById(
        [FromQuery] GetTripByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetTripById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTripByName")]
    [ResponseSchema<GetTripByNameResponse>]
    public async Task<IResult> GetTripByName(
        [FromQuery] GetTripByNameRequest request,
        CT ct)
    {
        var result = await _logic.GetTripByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTripByCode")]
    [ResponseSchema<GetTripByCodeResponse>]
    public async Task<IResult> GetTripByCode(
    [FromQuery] GetTripByCodeRequest request,
    CT ct)
    {
        var result = await _logic.GetTripByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveTripList")]
    [ResponseSchema<GetsActiveTripResponse>]
    public async Task<IResult> GetsActiveTripList(
        [FromQuery] GetsActiveTripRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredTrip")]
    [ResponseSchema<GetsFilteredTripResponse>]
    public async Task<IResult> GetsFilteredTrip(
        [FromQuery] GetsFilteredTripRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTrip(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTripExcelEnum")]
    [ResponseSchema<GetsTripExcelEnumResponse>]
    public async Task<IResult> GetsTripExcelEnum(
        [FromQuery] GetsTripExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTripExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTripExcelExporter")]
    [ResponseSchema<GetsTripExcelExporterResponse>]
    public async Task<IResult> GetsTripExcelExporter(
        [FromBody] GetsTripExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsTripExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableTrip")]
    [ResponseSchema<DisableTripResponse>]
    public async Task<IResult> DisableTrip(
        [FromQuery] DisableTripRequest request,
        CT ct)
    {
        var result = await _logic.DisableTrip(request, ct);
        return result.GetHttpResponse();
    }
}