using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.ServiceInfos;
using Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Models.ActiveService;
using Engineering.Application.Services.ServiceInfos.Models.CodeCreator;
using Engineering.Application.Services.ServiceInfos.Models.CreateService;
using Engineering.Application.Services.ServiceInfos.Models.DeleteServiceInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetActiveServices;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceById;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;
using Engineering.Application.Services.ServiceInfos.Models.GetServices;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByProjectOperationIds;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;
using Engineering.Application.Services.ServiceInfos.Models.InactiveService;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoGroupDelete;
using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;
using Engineering.Application.Services.ServiceInfos.Models.UpdateService;
using Engineering.Domain.Entities.ServiceInfos.Enums;
using MediatR;
using System.ComponentModel;

namespace Engineering.Api.Controllers.ServiceInfos;

[ApiController]
[Route("api/engineering/v1/ServiceInfo")]
public class ServiceInfoController : ControllerBase
{
    private readonly IServiceInfoLogic _logic;
    private readonly IMediator _mediator;

    public ServiceInfoController(IServiceInfoLogic logic)
    {
        _logic = logic;       
    }

    [HttpPost("AddServiceInfo")]
    [ResponseSchema<CreateServiceInfoResponse>]
    public async Task<IResult> AddServiceInfo([FromBody] CreateServiceInfoRequest request, CT ct)
    {
        var result = await _logic.CreateServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ServiceInfoCodeCreator")]
    [ResponseSchema<ServiceInfoCodeCreatorResponse>]
    public async Task<IResult> ServiceInfoCodeCreator([FromBody] ServiceInfoCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.CodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ServiceInfoGroupDelete")]
    [ResponseSchema<ServiceInfoGroupDeleteResponse>]
    public async Task<IResult> ServiceInfoGroupDelete([FromBody] ServiceInfoGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ServiceInfoGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditServiceInfo")]
    [ResponseSchema<UpdateServiceInfoResponse>]
    public async Task<IResult> EditServiceInfo([FromBody] UpdateServiceInfoRequest request, CT ct)
    {
        var result = await _logic.UpdateServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateServiceInfos")]
    [ResponseSchema<StateChangerServiceInfosResponse>]
    public async Task<IResult> ActivateServiceInfos([FromBody] ActivateServiceInfosRequest request, CT ct)
    {
        var result = await _logic.StateChangerServiceInfos(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateServiceInfos")]
    [ResponseSchema<StateChangerServiceInfosResponse>]
    public async Task<IResult> InactivateServiceInfos([FromBody] InactivateServiceInfosRequest request, CT ct)
    {
        var result = await _logic.StateChangerServiceInfos(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveServiceInfo")]
    [ResponseSchema<ActiveServiceInfoResponse>]
    public async Task<IResult> ActiveServiceInfo([FromBody] ActiveServiceInfoRequest request, CT ct)
    {
        var result = await _logic.ActiveServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveServiceInfo")]
    [ResponseSchema<InactiveServiceInfoResponse>]
    public async Task<IResult> InactiveServiceInfo([FromBody] InactiveServiceInfoRequest request, CT ct)
    {
        var result = await _logic.InactiveServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetServiceInfoById")]
    [ResponseSchema<GetServiceInfoByIdResponse>]
    public async Task<IResult> GetServiceInfoById([FromQuery] GetServiceInfoByIdRequest request, CT ct)
    {
        var result = await _logic.GetServiceInfoById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetServiceInfoByName")]
    [ResponseSchema<GetServiceInfoByNameResponse>]
    public async Task<IResult> GetServiceInfoByName([FromQuery] GetServiceInfoByNameRequest request, CT ct)
    {
        var result = await _logic.GetServiceInfoByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetServiceInfoByCode")]
    [ResponseSchema<GetServiceInfoByCodeResponse>]
    public async Task<IResult> GetServiceInfoByCode([FromQuery] GetServiceInfoByCodeRequest request, CT ct)
    {
        var result = await _logic.GetServiceInfoByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveServiceInfo")]
    [ResponseSchema<GetActiveServiceInfosResponse>]
    public async Task<IResult> GetsActiveServiceInfo([FromQuery] GetActiveServiceInfosRequest request, CT ct)
    {
        var result = await _logic.GetActiveServiceInfos(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsServiceInfo")]
    [ResponseSchema<GetServiceInfosResponse>]
    public async Task<IResult> GetsServiceInfo([FromQuery] GetServiceInfosRequest request, CT ct)
    {
        var result = await _logic.GetServiceInfos(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsServiceInfoByOperationInfoIds")]
    [ResponseSchema<GetsServiceInfoByOperationInfoResponse>]
    public async Task<IResult> GetsServiceInfoByOperationInfoIds([FromBody] GetsServiceInfoByOperationInfoRequest request, CT ct)
    {
        var result = await _logic.GetsServiceInfoByOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsServiceInfoByProjectOperationIds")]
    [ResponseSchema<GetsServiceInfoByProjectOperationIdsResponse>]
    public async Task<IResult> GetsServiceInfoByProjectOperationIds([FromBody] GetsServiceInfoByProjectOperationIdsRequest request, CT ct)
    {
        var result = await _logic.GetsServiceInfoByProjectOperationIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetServiceInfoDetail")]
    [ResponseSchema<SetServiceInfoDetailResponse>]
    public async Task<IResult> SetServiceInfoDetail(
        [FromBody] SetServiceInfoDetailRequest request, CT ct)
    {
        //var result = await _mediator.Send(
        //    new SetServiceInfoDetailCommand(
        //        request.Id,
        //        request.ServiceInfoEnName,
        //        request.DescriptionFa,
        //        request.DescriptionEn),
        //    ct);
        //return result.GetHttpResponse();
        var result = await _logic.SetServiceInfoDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsServiceInfoExcelExporter")]
    [ResponseSchema<GetsServiceInfoExcelExporterResponse>]
    public async Task<IResult> GetsServiceInfoExcelExporter([FromBody] GetsServiceInfoExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsServiceInfoExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsServiceInfoExcelEnum")]
    [ResponseSchema<GetsServiceInfoExcelEnumResponse>]
    public async Task<IResult> GetsServiceInfoExcelEnum([FromQuery] GetsServiceInfoExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsServiceInfoExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteServiceInfo")]
    [ResponseSchema<DeleteServiceInfoResponse>]
    public async Task<IResult> DeleteServiceInfo([FromQuery] DeleteServiceInfoRequest request, CT ct)
    {
        var result = await _logic.DeleteServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetServiceInfoType")]
    [Description("GetServiceInfoType.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetServiceInfoType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ServiceInfoType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }
}