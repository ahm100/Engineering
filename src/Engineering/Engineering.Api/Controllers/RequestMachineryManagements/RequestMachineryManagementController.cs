using Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryPaymentType;
using Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryContractor;
using Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryRequester;
using Engineering.Application.Services.RequestMachineryManagements;
using Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryInquieries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryManagements;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelEnum;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsTotalFilteredRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryBackToOnProject;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryConfirmed;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryDone;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryAppointment;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryDone;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryRejected;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryManagerConfirm;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOnProject;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryPending;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryRejected;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryResended;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryReturned;
using Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachinerySendToManager;
using Engineering.Application.Services.RequestMachineryManagements.Models.UpdateRequestMachineryAssignments;

namespace Engineering.Api.Controllers.RequestMachineryManagements;

[ApiController]
[Route("api/engineering/v1/RequestMachineryManagement")]
public class RequestMachineryManagementController : ControllerBase
{
    private readonly IRequestMachineryManagementLogic _logic;

    public RequestMachineryManagementController(IRequestMachineryManagementLogic logic)
    {
        _logic = logic;
    }

    [HttpGet("GetInquiries")]
    [ResponseSchema<GetRequestMachineryInquiriesResponse>]
    public async Task<IResult> GetInquiries([FromQuery] GetRequestMachineryInquiriesRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryInquiriesRequestAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetRequestMachineryManagementByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetRequestMachineryManagementByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryByIdAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFiltered")]
    [ResponseSchema<GetFilteredRequestMachineryManagementsResponse>]
    public async Task<IResult> GetFiltered([FromBody] GetFilteredRequestMachineryManagementsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestMachineriesAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredForInquiries")]
    [ResponseSchema<GetFilteredRequestMachineryInquieriesResponse>]
    public async Task<IResult> GetFilteredForInquiries([FromBody] GetFilteredRequestMachineryInquieriesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestMachineryInquieriesAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOperators")]
    [ResponseSchema<GetRequestMachineryInquiryOperatorsResponse>]
    public async Task<IResult> GetOperators([FromBody] GetRequestMachineryInquiryOperatorsRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryInquiryOperatorsAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestMachineryOperators")]
    [ResponseSchema<GetRequestMachineryOperatorsResponse>]
    public async Task<IResult> GetRequestMachineryOperators([FromBody] GetRequestMachineryOperatorsRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryOperatorsAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredMachineryRequesters")]
    [ResponseSchema<GetsFilteredMachineryRequesterResponse>]
    public async Task<IResult> GetFilteredMachineryRequesters([FromBody] GetsFilteredMachineryRequesterRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredMachineryRequesterAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredMachineryContractors")]
    [ResponseSchema<GetsFilteredMachineryContractorResponse>]
    public async Task<IResult> GetFilteredMachineryContractors([FromBody] GetsFilteredMachineryContractorRequest request, CT ct)
    {
        var result = await _logic.GetsFilteredMachineryContractorAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateInquiry")]
    [ResponseSchema<CreateRequestMachineryInquiryResponse>]
    public async Task<IResult> CreateInquiry([FromBody] CreateRequestMachineryInquiryRequest request, CT ct)
    {
        var result = await _logic.CreateRequestMachineryInquiryAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AssignMachineryForRequestMachinery")]
    [ResponseSchema<AssignMachineryForRequestMachineryResponse>]
    public async Task<IResult> AssignMachineryForRequestMachinery([FromBody] AssignMachineryForRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.AssignMachineryForRequestMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetOperator")]
    [ResponseSchema<SetRequestMachineryInquiryOperatorResponse>]
    public async Task<IResult> SetOperator([FromBody] SetRequestMachineryInquiryOperatorRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryInquiryOperatorAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Confirm")]
    [ResponseSchema<SetRequestMachineryConfirmedResponse>]
    public async Task<IResult> Confirm([FromBody] SetRequestMachineryConfirmedRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryConfirmedAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Pending")]
    [ResponseSchema<SetRequestMachineryPendingResponse>]
    public async Task<IResult> Pending([FromBody] SetRequestMachineryPendingRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryPendingAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Reject")]
    [ResponseSchema<SetRequestMachineryRejectedResponse>]
    public async Task<IResult> Reject([FromBody] SetRequestMachineryRejectedRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryRejectedAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InquiryReject")]
    [ResponseSchema<SetRequestMachineryInquiryRejectedResponse>]
    public async Task<IResult> InquiryReject([FromBody] SetRequestMachineryInquiryRejectedRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryInquiryRejectedAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InquiryDone")]
    [ResponseSchema<SetRequestMachineryInquiryDoneResponse>]
    public async Task<IResult> InquiryDone([FromBody] SetRequestMachineryInquiryDoneRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryInquiryDoneAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InquiryAppointment")]
    [ResponseSchema<SetRequestMachineryInquiryAppointmentResponse>]
    public async Task<IResult> InquiryAppointment([FromBody] SetRequestMachineryInquiryAppointmentRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryInquiryAppointmentAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("OnProject")]
    [ResponseSchema<SetRequestMachineryOnProjectResponse>]
    public async Task<IResult> OnProject([FromBody] SetRequestMachineryOnProjectRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryOnProjectAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Done")]
    [ResponseSchema<SetRequestMachineryDoneResponse>]
    public async Task<IResult> Done([FromBody] SetRequestMachineryDoneRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryDoneAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Returned")]
    [ResponseSchema<SetRequestMachineryReturnedResponse>]
    public async Task<IResult> Returned([FromBody] SetRequestMachineryReturnedRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryReturnedAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Resended")]
    [ResponseSchema<SetRequestMachineryResendedResponse>]
    public async Task<IResult> Resended([FromBody] SetRequestMachineryResendedRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryResendedAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestMachineryBackToOnProject")]
    [ResponseSchema<SetRequestMachineryBackToOnProjectResponse>]
    public async Task<IResult> SetRequestMachineryBackToOnProject([FromBody] SetRequestMachineryBackToOnProjectRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryBackToOnProject(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestMachineryManagerConfirm")]
    [ResponseSchema<SetRequestMachineryManagerConfirmResponse>]
    public async Task<IResult> SetRequestMachineryManagerConfirm([FromBody] SetRequestMachineryManagerConfirmRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachineryManagerConfirm(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetRequestMachinerySendToManager")]
    [ResponseSchema<SetRequestMachinerySendToManagerResponse>]
    public async Task<IResult> SetRequestMachinerySendToManager([FromBody] SetRequestMachinerySendToManagerRequest request, CT ct)
    {
        var result = await _logic.SetRequestMachinerySendToManager(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditRequestMachineryAssignments")]
    [ResponseSchema<UpdateRequestMachineryAssignmentsResponse>]
    public async Task<IResult> EditRequestMachineryAssignments([FromBody] UpdateRequestMachineryAssignmentsRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestMachineryAssignmentsAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestMachineryManagementExcelExporter")]
    [ResponseSchema<GetsRequestMachineryManagementExcelExporterResponse>]
    public async Task<IResult> GetsRequestMachineryManagementExcelExporter([FromBody] GetsRequestMachineryManagementExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsRequestMachineryManagementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestMachineryManagementExcelEnum")]
    [ResponseSchema<GetsRequestMachineryManagementExcelEnumResponse>]
    public async Task<IResult> GetsRequestMachineryManagementExcelEnum([FromQuery] GetsRequestMachineryManagementExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsRequestMachineryManagementExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalFilteredRequestMachinery")]
    [ResponseSchema<GetsTotalFilteredRequestMachineryResponse>]
    public async Task<IResult> GetsTotalFilteredRequestMachinery([FromBody] GetsTotalFilteredRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.GetsTotalFilteredRequestMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestMachineryPaymentType")]
    [ResponseSchema<GetRequestMachineryPaymentTypeResponse>]
    public async Task<IResult> GetRequestMachineryPaymentType([FromQuery] GetRequestMachineryPaymentTypeRequest request, CT ct)
    {
        var result = await _logic.GetRequestMachineryPaymentType(request, ct);
        return result.GetHttpResponse();
    }
}