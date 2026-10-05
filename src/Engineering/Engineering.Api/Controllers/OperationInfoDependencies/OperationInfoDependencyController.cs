using Engineering.Application.Services.OperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetsOperationInfoDependencyType;
using Engineering.Application.Services.OperationInfoDependencies.Models.UpdateOperationInfoDependency;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfoDependency")]
public class OperationInfoDependencyController : ControllerBase
{
    private readonly IOperationInfoDependencyLogic _logic;

    public OperationInfoDependencyController(IOperationInfoDependencyLogic logic)
    {
        _logic = logic;
    }

    [HttpPut("EditOperationInfoDependency")]
    [ResponseSchema<UpdateOperationInfoDependencyResponse>]
    public async Task<IResult> EditOperationInfoDependency([FromBody] UpdateOperationInfoDependencyRequest request, CT ct)
    {
        var result = await _logic.UpdateOperationInfoDependency(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoDependencyById")]
    [ResponseSchema<GetOperationInfoDependencyByIdResponse>]
    public async Task<IResult> GetOperationInfoDependencyById([FromQuery] GetOperationInfoDependencyByIdRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoDependencyById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoDependency")]
    [ResponseSchema<GetOperationInfoDependenciesResponse>]
    public async Task<IResult> GetsOperationInfoDependency([FromQuery] GetOperationInfoDependenciesRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoDependencies(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoDependencyType")]
    [ResponseSchema<GetsOperationInfoDependencyTypeResponse>]
    public async Task<IResult> GetsOperationInfoDependencyType([FromQuery] GetsOperationInfoDependencyTypeRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoDependencyType(request, ct);
        return result.GetHttpResponse();
    }
}