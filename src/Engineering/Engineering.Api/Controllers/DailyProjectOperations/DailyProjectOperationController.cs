using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.DailyProjectOperationTelegramMessageSender;
using Engineering.Application.Services.DailyProjectOperations.Models.DeleteDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationByLegacyId;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationContractors;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetEmployerStatusStatementLimitDate;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableExperts;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperationDetails;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredMachineries;
using Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredProjectOperationsByProjectIds;
using Engineering.Application.Services.DailyProjectOperations.Models.GetOperationTotals;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationStatus;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetTotalsByProjectOperationDetailId;
using Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperationDocuments;
using Engineering.Application.Services.TransportationRequests.Models.GetsDailyCreator;
using Engineering.Domain.Entities.DailyProjectOperations;
using System.ComponentModel;

[ApiController]
[Route("api/engineering/v1/DailyProjectOperation")]
public class DailyProjectOperationController : ControllerBase
{
    private readonly IDailyProjectOperationLogic _logic;

    public DailyProjectOperationController(IDailyProjectOperationLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateDailyProjectOperationResponse>]
    public async Task<IResult> Create([FromBody] CreateDailyProjectOperationRequest request, CT ct)
    {
        var result = await _logic.CreateDailyProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateDailyProjectOperationResponse>]
    public async Task<IResult> Update([FromBody] UpdateDailyProjectOperationRequest request, CT ct)
    {
        var result = await _logic.UpdateDailyProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("DailyProjectOperationTelegramMessageSender")]
    [ResponseSchema<DailyProjectOperationTelegramMessageSenderResponse>]
    public async Task<IResult> DailyProjectOperationTelegramMessageSender([FromBody] DailyProjectOperationTelegramMessageSenderRequest request, CT ct)
    {
        var result = await _logic.DailyProjectOperationTelegramMessageSender(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateDailyProjectOperationDocuments")]
    [ResponseSchema<UpdateDailyProjectOperationDocumentsResponse>]
    public async Task<IResult> UpdateDailyProjectOperationDocuments([FromBody] UpdateDailyProjectOperationDocumentsRequest request, CT ct)
    {
        var result = await _logic.UpdateDailyProjectOperationDocuments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetDailyProjectOperationByIdResponse>]
    public async Task<IResult> GetById([FromQuery] GetDailyProjectOperationByIdRequest request, CT ct)
    {
        var result = await _logic.GetDailyProjectOperationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("Histories")]
    [ResponseSchema<GetDailyProjectOperationHistoriesResponse>]
    public async Task<IResult> Histories([FromQuery] GetDailyProjectOperationHistoriesRequest request, CT ct)
    {
        var result = await _logic.GetDailyProjectOperationHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectOperationsByProjectIds")]
    [ResponseSchema<GetFilteredProjectOperationsByProjectIdsResponse>]
    public async Task<IResult> GetProjectOperationsByProjectIds([FromBody] GetFilteredProjectOperationsByProjectIdsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredProjectOperationsByProjectIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectOperations")]
    [ResponseSchema<GetFilteredDailyProjectOperationsResponse>]
    public async Task<IResult> GetProjectOperations([FromBody] GetFilteredDailyProjectOperationsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredDailyProjectOperations(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectOperationDetails")]
    [ResponseSchema<GetFilteredDailyProjectOperationDetailsResponse>]
    public async Task<IResult> GetProjectOperationDetails([FromBody] GetFilteredDailyProjectOperationDetailsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredDailyProjectOperationDetails(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredConsumableMachineries")]
    [ResponseSchema<GetFilteredMachineriesResponse>]
    public async Task<IResult> GetFilteredConsumableMachineries([FromBody] GetFilteredMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredMachineries(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredConsumableExperts")]
    [ResponseSchema<GetFilteredConsumableExpertsResponse>]
    public async Task<IResult> GetFilteredConsumableExperts([FromBody] GetFilteredConsumableExpertsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredConsumableExperts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredConsumableProducts")]
    [ResponseSchema<GetFilteredConsumableProductsResponse>]
    public async Task<IResult> GetFilteredConsumableProducts([FromBody] GetFilteredConsumableProductsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredConsumableProducts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDailyProjectOperationStatus")]
    [ResponseSchema<GetsDailyProjectOperationStatusResponse>]
    public async Task<IResult> GetsDailyProjectOperationStatus([FromQuery] GetsDailyProjectOperationStatusRequest request, CT ct)
    {
        var result = await _logic.GetsDailyProjectOperationStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetEmployerStatusStatementLimitDate")]
    [ResponseSchema<GetEmployerStatusStatementLimitDateResponse>]
    public async Task<IResult> GetEmployerStatusStatementLimitDate([FromQuery] GetEmployerStatusStatementLimitDateRequest request, CT ct)
    {
        var result = await _logic.GetEmployerStatusStatementLimitDate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDailyProjectOperationByLegacyId")]
    [ResponseSchema<GetDailyProjectOperationByLegacyIdResponse>]
    public async Task<IResult> GetDailyProjectOperationByLegacyId([FromQuery] GetDailyProjectOperationByLegacyIdRequest request, CT ct)
    {
        var result = await _logic.GetDailyProjectOperationByLegacyId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDailyProjectOperationContractors")]
    [ResponseSchema<GetDailyProjectOperationContractorsResponse>]
    public async Task<IResult> GetDailyProjectOperationContractors([FromBody] GetDailyProjectOperationContractorsRequest request, CT ct)
    {
        var result = await _logic.GetDailyProjectOperationContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDetailedDailyProjectOperation")]
    [ResponseSchema<GetsDetailedDailyProjectOperationResponse>]
    public async Task<IResult> GetsDetailedDailyProjectOperation([FromBody] GetsDetailedDailyProjectOperationRequest request, CT ct)
    {
        var result = await _logic.GetsDetailedDailyProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDailyHistoriesExcelExporter")]
    [ResponseSchema<GetDailyHistoriesExcelExporterResponse>]
    public async Task<IResult> GetDailyHistoriesExcelExporter([FromBody] GetDailyHistoriesExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetDailyHistoriesExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDailyHistoriesExcelEnums")]
    [ResponseSchema<GetDailyHistoriesExcelEnumsResponse>]
    public async Task<IResult> GetDailyHistoriesExcelEnums([FromQuery] GetDailyHistoriesExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetDailyHistoriesExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDetailedDailyProjectOperationExcelExporter")]
    [ResponseSchema<GetsDetailedDailyProjectOperationExcelExporterResponse>]
    public async Task<IResult> GetsDetailedDailyProjectOperationExcelExporter([FromBody] GetsDetailedDailyProjectOperationExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsDetailedDailyProjectOperationExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDetailedDailyProjectOperationExcelEnums")]
    [ResponseSchema<GetsDetailedDailyProjectOperationExcelEnumsResponse>]
    public async Task<IResult> GetsDetailedDailyProjectOperationExcelEnums([FromQuery] GetsDetailedDailyProjectOperationExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetsDetailedDailyProjectOperationExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDailyProjectOperationDocument")]
    [ResponseSchema<GetsDailyProjectOperationDocumentResponse>]
    public async Task<IResult> GetsDailyProjectOperationDocument([FromQuery] GetsDailyProjectOperationDocumentRequest request, CT ct)
    {
        var result = await _logic.GetsDailyProjectOperationDocument(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTotalsByProjectOperationDetailId")]
    [ResponseSchema<GetTotalsByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetTotalsByProjectOperationDetailId([FromQuery] GetTotalsByProjectOperationDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetTotalsByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationTotals")]
    [ResponseSchema<GetOperationTotalsResponse>]
    public async Task<IResult> GetOperationTotals([FromQuery] GetOperationTotalsRequest request, CT ct)
    {
        var result = await _logic.GetOperationTotals(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDetailedDailyOperationTotals")]
    [ResponseSchema<GetDetailedDailyOperationTotalsResponse>]
    public async Task<IResult> GetDetailedDailyOperationTotals([FromBody] GetDetailedDailyOperationTotalsRequest request, CT ct)
    {
        var result = await _logic.GetDetailedDailyOperationTotals(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDailyProjectOperationService")]
    [ResponseSchema<GetsDailyProjectOperationServiceResponse>]
    public async Task<IResult> GetsDailyProjectOperationService([FromBody] GetsDailyProjectOperationServiceRequest request, CT ct)
    {
        var result = await _logic.GetsDailyProjectOperationService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTotalDailyProjectOperationService")]
    [ResponseSchema<GetsTotalDailyProjectOperationServiceResponse>]
    public async Task<IResult> GetsTotalDailyProjectOperationService([FromBody] GetsTotalDailyProjectOperationServiceRequest request, CT ct)
    {
        var result = await _logic.GetsTotalDailyProjectOperationService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDailyProjectOperationServiceExcelExporter")]
    [ResponseSchema<GetsDailyProjectOperationServiceExcelExporterResponse>]
    public async Task<IResult> GetsDailyProjectOperationServiceExcelExporter([FromBody] GetsDailyProjectOperationServiceExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsDailyProjectOperationServiceExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDailyProjectOperationServiceExcelEnums")]
    [ResponseSchema<GetsDailyProjectOperationServiceExcelEnumsResponse>]
    public async Task<IResult> GetsDailyProjectOperationServiceExcelEnums([FromQuery] GetsDailyProjectOperationServiceExcelEnumsRequest request, CT ct)
    {
        var result = await _logic.GetsDailyProjectOperationServiceExcelEnums(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDailyServiceInfo")]
    [ResponseSchema<GetsDailyServiceInfoResponse>]
    public async Task<IResult> GetsDailyServiceInfo([FromBody] GetsDailyServiceInfoRequest request, CT ct)
    {
        var result = await _logic.GetsDailyServiceInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDailyCreator")]
    [ResponseSchema<GetsDailyCreatorResponse>]
    public async Task<IResult> GetsDailyCreator([FromQuery] GetsDailyCreatorRequest request, CT ct)
    {
        var result = await _logic.GetsDailyCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteDailyProjectOperation")]
    [ResponseSchema<DeleteDailyProjectOperationResponse>]
    public async Task<IResult> DeleteDailyProjectOperation([FromQuery] DeleteDailyProjectOperationRequest request, CT ct)
    {
        var result = await _logic.DeleteDailyProjectOperation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDailyProjectOperationHistoryById")]
    [ResponseSchema<GetDailyProjectOperationHistoryByIdResponse>]
    public async Task<IResult> GetDailyProjectOperationHistoryById([FromQuery] GetDailyProjectOperationHistoryByIdRequest request, CT ct)
    {
        var result = await _logic.GetDailyProjectOperationHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDailyProjectOperationType")]
    [Description("Get enum values for  DailyProjectOperationType filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetDailyProjectOperationType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<DailyProjectOperationType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

}