using Engineering.Application.Services.RequestContractors;
using Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorStatus;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelExporter;
using Engineering.Application.Services.RequestContractors.Models.GroupRequestContractorStatusChanger;
using Engineering.Application.Services.RequestContractors.Models.RequestContractorGroupDelete;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorEndInquiry;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryRejected;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorPending;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorRejected;
using Engineering.Application.Services.RequestContractors.Models.UpdateRequestContractor;

namespace Engineering.Api.Controllers.RequestContractors;

[ApiController]
[Route("api/engineering/v1/RequestContractor")]
public class RequestContractorController : ControllerBase
{
    private readonly IRequestContractorLogic _logic;

    public RequestContractorController(IRequestContractorLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateRequestContractor")]
    [ResponseSchema<CreateRequestContractorResponse>]
    public async Task<IResult> CreateRequestContractor(
    [FromBody] CreateRequestContractorRequest request,
    CT ct)
    {
        var result = await _logic.CreateRequestContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteRequestContractor")]
    [ResponseSchema<DeleteRequestContractorResponse>]
    public async Task<IResult> DeleteRequestContractor(
        [FromBody] DeleteRequestContractorRequest request,
        CT ct)
    {
        var result = await _logic.DeleteRequestContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateRequestContractor")]
    [ResponseSchema<UpdateRequestContractorResponse>]
    public async Task<IResult> UpdateRequestContractor(
        [FromBody] UpdateRequestContractorRequest request,
        CT ct)
    {
        var result = await _logic.UpdateRequestContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RequestContractorGroupDelete")]
    [ResponseSchema<RequestContractorGroupDeleteResponse>]
    public async Task<IResult> RequestContractorGroupDelete(
        [FromBody] RequestContractorGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.RequestContractorGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GroupRequestContractorStatusChanger")]
    [ResponseSchema<GroupRequestContractorStatusChangerResponse>]
    public async Task<IResult> GroupRequestContractorStatusChanger(
        [FromBody] GroupRequestContractorStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.GroupRequestContractorStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorConfirmed")]
    [ResponseSchema<SetRequestContractorConfirmedResponse>]
    public async Task<IResult> SetRequestContractorConfirmed(
        [FromBody] SetRequestContractorConfirmedRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorConfirmed(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorInquiryConfirmed")]
    [ResponseSchema<SetRequestContractorInquiryConfirmedResponse>]
    public async Task<IResult> SetRequestContractorInquiryConfirmed(
        [FromBody] SetRequestContractorInquiryConfirmedRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorInquiryConfirmed(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorPending")]
    [ResponseSchema<SetRequestContractorPendingResponse>]
    public async Task<IResult> SetRequestContractorPending(
        [FromBody] SetRequestContractorPendingRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorPending(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorInquiryRejected")]
    [ResponseSchema<SetRequestContractorInquiryRejectedResponse>]
    public async Task<IResult> SetRequestContractorInquiryRejected(
        [FromBody] SetRequestContractorInquiryRejectedRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorInquiryRejected(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorRejected")]
    [ResponseSchema<SetRequestContractorRejectedResponse>]
    public async Task<IResult> SetRequestContractorRejected(
        [FromBody] SetRequestContractorRejectedRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorRejected(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestContractorEndInquiry")]
    [ResponseSchema<SetRequestContractorEndInquiryResponse>]
    public async Task<IResult> SetRequestContractorEndInquiry(
        [FromBody] SetRequestContractorEndInquiryRequest request,
        CT ct)
    {
        var result = await _logic.SetRequestContractorEndInquiry(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestContractorStatus")]
    [ResponseSchema<GetRequestContractorStatusResponse>]
    public async Task<IResult> GetRequestContractorStatus(
        [FromQuery] GetRequestContractorStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestContractorStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestContractorExcelExporter")]
    [ResponseSchema<GetsRequestContractorExcelExporterResponse>]
    public async Task<IResult> GetsRequestContractorExcelExporter(
        [FromBody] GetsRequestContractorExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestContractorExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestContractorExcelEnums")]
    [ResponseSchema<GetsRequestContractorExcelEnumsResponse>]
    public async Task<IResult> GetsRequestContractorExcelEnums(
        [FromQuery] GetsRequestContractorExcelEnumsRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestContractorExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredRequestContractors")]
    [ResponseSchema<GetFilteredRequestContractorsResponse>]
    public async Task<IResult> GetFilteredRequestContractors(
        [FromBody] GetFilteredRequestContractorsRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredRequestContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestContractorHistories")]
    [ResponseSchema<GetRequestContractorHistoriesResponse>]
    public async Task<IResult> GetRequestContractorHistories(
        [FromQuery] GetRequestContractorHistoriesRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestContractorHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestContractorById")]
    [ResponseSchema<GetRequestContractorByIdResponse>]
    public async Task<IResult> GetRequestContractorById(
        [FromQuery] GetRequestContractorByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestContractorById(request, ct);
        return result.GetHttpResponse();
    }
}