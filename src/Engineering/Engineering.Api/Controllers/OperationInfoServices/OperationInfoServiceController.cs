using Engineering.Application.Services.OperationInfoServices;
using Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.DeleteOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceFiltered;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfoService")]
public class OperationInfoServiceController : ControllerBase
{
    private readonly IOperationInfoServiceLogic _logic;

    public OperationInfoServiceController(IOperationInfoServiceLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationInfoService")]
    [ResponseSchema<CreateOperationInfoServiceResponse>]
    public async Task<IResult> AddOperationInfoService(
    [FromBody] CreateOperationInfoServiceRequest request,
    CT ct)
    {
        var newRequest = new CreateOperationInfoServiceModelRequest(request.OperationInfoIds, null, request.ServiceInfos);
        var result = await _logic.CreateOperationInfoService(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoService")]
    [ResponseSchema<GetsByOperationInfoIdResponse>]
    public async Task<IResult> GetsOperationInfoService(
        [FromQuery] GetsByOperationInfoIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoServiceFiltered")]
    [ResponseSchema<GetsOperationInfoServiceFilteredResponse>]
    public async Task<IResult> GetsOperationInfoServiceFiltered(
        [FromQuery] GetsOperationInfoServiceFilteredRequest request,
        CT ct)
    {
        var result = await _logic.GetsOperationInfoServiceFiltered(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoServiceByProjectId")]
    [ResponseSchema<GetsOperationInfoServiceByProjectIdResponse>]
    public async Task<IResult> GetsOperationInfoServiceByProjectId(
        [FromQuery] GetsOperationInfoServiceByProjectIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsOperationInfoServiceByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteOperationInfoService")]
    [ResponseSchema<DeleteOperationInfoServiceResponse>]
    public async Task<IResult> DeleteOperationInfoService(
        [FromBody] DeleteOperationInfoServiceRequest request,
        CT ct)
    {
        var result = await _logic.DeleteOperationInfoService(request, ct);
        return result.GetHttpResponse();
    }
}