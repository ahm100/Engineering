using Engineering.Application.Services.EmployerStatusStatements;
using Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;
using Engineering.Application.Services.EmployerStatusStatements.Models.CreateEmployerStatusStatement;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementCodeCreator;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementStatus;
using Engineering.Application.Services.EmployerStatusStatements.Models.EmployerStatusStatementType;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementById;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementHistory;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperation;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetail;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailDaily;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerRequester;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerStatusStatement;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateEmployerStatusStatementDocuments;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailiesDocument;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationDetailDailyVolume;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsDocument;
using Engineering.Application.Services.EmployerStatusStatements.Models.UpdateESSProjectOperationsZeroVolume;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Api.Controllers.EmployerStatusStatements;

[ApiController]
[Route("api/engineering/v1/EmployerStatusStatement")]
public class EmployerStatusStatementController : ControllerBase
{
    private readonly IEmployerStatusStatementLogic _logic;

    public EmployerStatusStatementController(IEmployerStatusStatementLogic logic)
    {
        _logic = logic;
    }

    [ResponseSchema<CreateEmployerStatusStatementResponse>]
    [HttpPost("CreateEmployerStatusStatement")]
    public async Task<IResult> CreateEmployerStatusStatement(
    [FromBody] CreateEmployerStatusStatementRequest request,
    CT ct)
    {
        var result = await _logic.CreateEmployerStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<EmployerStatusStatementCodeCreatorResponse>]
    [HttpPost("EmployerStatusStatementCodeCreator")]
    public async Task<IResult> EmployerStatusStatementCodeCreator(
        [FromBody] EmployerStatusStatementCodeCreatorRequest request,
        CT ct)
    {
        var result = await _logic.EmployerStatusStatementCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateESSProjectOperationsZeroVolumeResponse>]
    [HttpPut("UpdateESSProjectOperationsZeroVolume")]
    public async Task<IResult> UpdateESSProjectOperationsZeroVolume(
        [FromBody] UpdateESSProjectOperationsZeroVolumeRequest request,
        CT ct)
    {
        var result = await _logic.UpdateESSProjectOperationsZeroVolume(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateESSProjectOperationsDocumentResponse>]
    [HttpPut("UpdateESSProjectOperationsDocument")]
    public async Task<IResult> UpdateESSProjectOperationsDocument(
        [FromBody] UpdateESSProjectOperationsDocumentRequest request,
        CT ct)
    {
        var result = await _logic.UpdateESSProjectOperationsDocument(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateESSProjectOperationDetailDailyVolumeResponse>]
    [HttpPut("UpdateESSProjectOperationDetailDailyVolume")]
    public async Task<IResult> UpdateESSProjectOperationDetailDailyVolume(
        [FromBody] UpdateESSProjectOperationDetailDailyVolumeRequest request,
        CT ct)
    {
        var result = await _logic.UpdateESSProjectOperationDetailDailyVolume(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateESSProjectOperationDetailDailiesDocumentResponse>]
    [HttpPut("UpdateESSProjectOperationDetailDailiesDocument")]
    public async Task<IResult> UpdateESSProjectOperationDetailDailiesDocument(
        [FromBody] UpdateESSProjectOperationDetailDailiesDocumentRequest request,
        CT ct)
    {
        var result = await _logic.UpdateESSProjectOperationDetailDailiesDocument(request, ct);
        return result.GetHttpResponse();
    }

    [ResponseSchema<UpdateEmployerStatusStatementDocumentsResponse>]
    [HttpPut("UpdateEmployerStatusStatementDocuments")]
    public async Task<IResult> UpdateEmployerStatusStatementDocuments(
        [FromBody] UpdateEmployerStatusStatementDocumentsRequest request,
        CT ct)
    {
        var result = await _logic.UpdateEmployerStatusStatementDocuments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToConsultantPending")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToConsultantPending(
        [FromBody] ChangeESSStatusToConsultantPendingRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.ConsultantPending, null, null, null);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToEmployerRepresentativePending")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToEmployerRepresentativePending(
        [FromBody] ChangeESSStatusToEmployerRepresentativePendingRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.EmployerRepresentativePending, null, null, null);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToInvalidated")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToInvalidated(
        [FromBody] ChangeESSStatusToInvalidatedRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.Invalidated, null, null, request.Discription);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToPending")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToPending(
        [FromBody] ChangeESSStatusToPendingRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.Pending, null, null, null);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToReturnForReview")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToReturnForReview(
        [FromBody] ChangeESSStatusToReturnForReviewRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.ReturnForReview, null, null, request.Discription);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToSendToConsultant")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToSendToConsultant(
        [FromBody] ChangeESSStatusToSendToConsultantRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.SendToConsultant, request.ESSProjectOperations, request.ESSProjectOperationDetailDailies, request.Discription);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToSendToEmployerRepresentative")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToSendToEmployerRepresentative(
    [FromBody] ChangeESSStatusToSendToEmployerRepresentativeRequest request,
    CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.SendToEmployerRepresentative, request.ESSProjectOperations, request.ESSProjectOperationDetailDailies, request.Discription);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToSendToSupervisor")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToSendToSupervisor(
        [FromBody] ChangeESSStatusToSendToSupervisorRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.SendToSupervisor, request.ESSProjectOperations, request.ESSProjectOperationDetailDailies, request.Discription);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeESSStatusToSupervisorPending")]
    [ResponseSchema<ChangeESSStatusResponse>]
    public async Task<IResult> ChangeESSStatusToSupervisorPending(
        [FromBody] ChangeESSStatusToSupervisorPendingRequest request,
        CT ct)
    {
        var requestModel = new ChangeESSStatusRequest(request.Id, EmployerStatusStatementStatus.SupervisorPending, null, null, null);
        var result = await _logic.ChangeESSStatus(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredEmployerStatusStatement")]
    [ResponseSchema<GetsFilteredEmployerStatusStatementResponse>]
    public async Task<IResult> GetsFilteredEmployerStatusStatement(
        [FromBody] GetsFilteredEmployerStatusStatementRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredEmployerStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsEmployerStatusStatementProjectOperationDetailDaily")]
    [ResponseSchema<GetsEmployerStatusStatementProjectOperationDetailDailyResponse>]
    public async Task<IResult> GetsEmployerStatusStatementProjectOperationDetailDaily(
        [FromBody] GetsEmployerStatusStatementProjectOperationDetailDailyRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementProjectOperationDetailDaily(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetEmployerStatusStatementById")]
    [ResponseSchema<GetEmployerStatusStatementByIdResponse>]
    public async Task<IResult> GetEmployerStatusStatementById(
        [FromQuery] GetEmployerStatusStatementByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetEmployerStatusStatementById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsEmployerStatusStatementHistory")]
    [ResponseSchema<GetsEmployerStatusStatementHistoryResponse>]
    public async Task<IResult> GetsEmployerStatusStatementHistory(
        [FromQuery] GetsEmployerStatusStatementHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredEmployerRequester")]
    [ResponseSchema<GetsFilteredEmployerRequesterResponse>]
    public async Task<IResult> GetsFilteredEmployerRequester(
        [FromQuery] GetsFilteredEmployerRequesterRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredEmployerRequester(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsEmployerStatusStatementStatus")]
    [ResponseSchema<GetsEmployerStatusStatementStatusResponse>]
    public async Task<IResult> GetsEmployerStatusStatementStatus(
        [FromQuery] GetsEmployerStatusStatementStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsEmployerStatusStatementType")]
    [ResponseSchema<GetsEmployerStatusStatementTypeResponse>]
    public async Task<IResult> GetsEmployerStatusStatementType(
        [FromQuery] GetsEmployerStatusStatementTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsEmployerStatusStatementProjectOperation")]
    [ResponseSchema<GetsEmployerStatusStatementProjectOperationResponse>]
    public async Task<IResult> GetsEmployerStatusStatementProjectOperation(
        [FromQuery] GetsEmployerStatusStatementProjectOperationRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsEmployerStatusStatementProjectOperationDetail")]
    [ResponseSchema<GetsEmployerStatusStatementProjectOperationDetailResponse>]
    public async Task<IResult> GetsEmployerStatusStatementProjectOperationDetail(
        [FromQuery] GetsEmployerStatusStatementProjectOperationDetailRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementProjectOperationDetail(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsEmployerStatusStatementExcelExporter")]
    [ResponseSchema<GetsEmployerStatusStatementExcelExporterResponse>]
    public async Task<IResult> GetsEmployerStatusStatementExcelExporter(
        [FromBody] GetsEmployerStatusStatementExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsEmployerStatusStatementExcelEnums")]
    [ResponseSchema<GetsEmployerStatusStatementExcelEnumsResponse>]
    public async Task<IResult> GetsEmployerStatusStatementExcelEnums(
        [FromBody] GetsEmployerStatusStatementExcelEnumsRequest request,
        CT ct)
    {
        var result = await _logic.GetsEmployerStatusStatementExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetEmployerStatusStatementExcelExporter")]
    [ResponseSchema<GetEmployerStatusStatementExcelExporterResponse>]
    public async Task<IResult> GetEmployerStatusStatementExcelExporter(
        [FromQuery] GetEmployerStatusStatementExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetEmployerStatusStatementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetEmployerStatusStatementLetterheadExcel")]
    [ResponseSchema<GetEmployerStatusStatementLetterheadExcelResponse>]
    public async Task<IResult> GetEmployerStatusStatementLetterheadExcel(
        [FromQuery] GetEmployerStatusStatementLetterheadExcelRequest request,
        CT ct)
    {
        var result = await _logic.GetEmployerStatusStatementLetterheadExcel(request, ct);
        return result.GetHttpResponse();
    }
}