using Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetDetailByRGSTypeId;
using Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetFltrRGS;
using Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetReferenceTypeHistory;
using Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetRGSTypeByRGSId;
using Engineering.Api.Controllers.RequestGoodsSupplies.Reports.GetDetailByRGSIdReport;
using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateProjectRequestGoodsSuppliesDraft;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupplyDraft;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Pdf;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Xslx;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGSupplyManagement;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGSupplyProjectManager;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyStatus;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RequestGoodsSuppliesGroupDelete;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetGoodsSupplyToCreated;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetOperationInfoSeasonToGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceItems;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using MediatR;
using System.ComponentModel;

namespace Engineering.Api.Controllers.RequestGoodsSupplies;

[ApiController]
[Route("api/engineering/v1/RequestGoodsSupply")]
public class RequestGoodsSupplyController : ControllerBase
{
    private readonly IRequestGoodsSupplyLogic _logic;
    private readonly ILogger<RequestGoodsSupplyController> _logger;
    private readonly GetDetailByRGSIdXslxReportHandle _detailExcel;
    private readonly GetReferenceTypeHistoryFaReportHandle _referenceHistoryFaPdf;
    private readonly GetDetailByRGSIdXslxEnReportHandle _detailEnExcel;
    private readonly GetDetailByRGSIdPdfReportHandle _detailPdf;
    private readonly GetDetailByRGSIdPdfEnReportHandle _detailEnPdf;
    private readonly IMediator _mediator;

    public RequestGoodsSupplyController(
        IRequestGoodsSupplyLogic logic,
        IMediator mediator,
        ILogger<RequestGoodsSupplyController> logger,
        GetDetailByRGSIdXslxReportHandle detailExcel,
        GetDetailByRGSIdPdfReportHandle detailPdf,
        GetReferenceTypeHistoryFaReportHandle referenceHistoryFaPdf,
        GetDetailByRGSIdPdfEnReportHandle detailEnPdf,
        GetDetailByRGSIdXslxEnReportHandle detailEnExcel)
    {
        _logic = logic;
        _logger = logger;
        _mediator = mediator;
        _detailExcel = detailExcel;
        _detailPdf = detailPdf;
        _detailEnPdf = detailEnPdf;
        _detailEnExcel = detailEnExcel;
        _referenceHistoryFaPdf = referenceHistoryFaPdf;
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateRequestGoodsSupplyResponse>]
    public async Task<IResult> Create(
    [FromBody] CreateRequestGoodsSupplyRequest request, CT ct)
    {
        var result = await _logic.CreateRequestGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateRequestGoodsSupplyDraft")]
    [ResponseSchema<CreateRequestGoodsSupplyResponse>]
    public async Task<IResult> CreateRequestGoodsSupplyDraft(
        [FromBody] CreateRequestGoodsSupplyDraftRequest request, CT ct)
    {
        var newRequest = request.Adapt<CreateRequestGoodsSupplyRequest>();
        newRequest.IsDraft = true;
        var result = await _logic.CreateRequestGoodsSupply(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectRequestGoodsSupply")]
    [ResponseSchema<CreateProjectRequestGoodsSuppliesResponse>]
    public async Task<IResult> CreateProjectRequestGoodsSupply(
        [FromBody] CreateProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        var result = await _logic.CreateProjectRequestGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectRequestGoodsSupplyDraft")]
    [ResponseSchema<CreateProjectRequestGoodsSuppliesResponse>]
    public async Task<IResult> CreateProjectRequestGoodsSupplyDraft(
        [FromBody] CreateProjectRequestGoodsSupplyDraftRequest request, CT ct)
    {
        request.IsDraft = true;
        var result = await _logic.CreateProjectRequestGoodsSupply(request.Adapt<CreateProjectRequestGoodsSuppliesRequest>(), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateProjectRequestGoodsSupply")]
    [ResponseSchema<UpdateProjectRequestGoodsSuppliesResponse>]
    public async Task<IResult> UpdateProjectRequestGoodsSupply(
        [FromBody] UpdateProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectRequestGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SetGoodsSupplyToCreated")]
    [ResponseSchema<SetGoodsSupplyToCreatedResponse>]
    public async Task<IResult> SetGoodsSupplyToCreated(
        [FromBody] SetGoodsSupplyToCreatedRequest request, CT ct)
    {
        var result = await _logic.SetGoodsSupplyToCreated(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("RGSupplyDetailsStatusChanger")]
    [ResponseSchema<RGSupplyDetailsStatusChangerResponse>]
    public async Task<IResult> RGSupplyDetailsStatusChanger(
        [FromBody] RGSupplyDetailsStatusChangerRequest request, CT ct)
    {
        var result = await _logic.RGSupplyDetailsStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RequestGoodsSupplyGroupDelete")]
    [ResponseSchema<RequestGoodsSuppliesGroupDeleteResponse>]
    public async Task<IResult> RequestGoodsSupplyGroupDelete(
        [FromBody] RequestGoodsSuppliesGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.RequestGoodsSuppliesGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SetOperationInfoSeasonToGoodsSupply")]
    [ResponseSchema<SetOperationInfoSeasonToGoodsSupplyResponse>]
    public async Task<IResult> SetOperationInfoSeasonToGoodsSupply(
        [FromBody] SetOperationInfoSeasonToGoodsSupplyRequest request, CT ct)
    {
        var result = await _logic.SetOperationInfoSeasonToGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrRGSupplyWithProducts")]
    [ResponseSchema<GetFltrRGSupplyWithProductsResponse>]
    public async Task<IResult> GetFltrRGSupplyWithProducts(
        [FromBody] GetFltrRGSupplyWithProductsRequest request, CT ct)
    {
        var result = await _logic.GetFltrRGSupplyWithProducts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrRGSupplyManagement")]
    [ResponseSchema<GetFltrRGSupplyWithProductsResponse>]
    public async Task<IResult> GetFltrRGSupplyManagement(
        [FromBody] GetFltrRGSupplyManagementRequest request, CT ct)
    {
        var req = request.Adapt<GetFltrRGSupplyWithProductsRequest>();
        req.Statuses ??= new List<GoodsSupplyDetailStatus>
        {
            (GoodsSupplyDetailStatus)33,
            (GoodsSupplyDetailStatus)66
        };
        req.RemoveStatuses ??= new List<GoodsSupplyDetailStatus>
        {
            (GoodsSupplyDetailStatus)1,
            (GoodsSupplyDetailStatus)11,
            (GoodsSupplyDetailStatus)22,
            (GoodsSupplyDetailStatus)44,
            (GoodsSupplyDetailStatus)55
        };
        req.Types = new List<GoodsSupplyType>
        {
            (GoodsSupplyType)2
        };

        var result = await _logic.GetFltrRGSupplyWithProducts(req, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrRGSupplyProjectManager")]
    [ResponseSchema<GetFltrRGSupplyWithProductsResponse>]
    public async Task<IResult> GetFltrRGSupplyProjectManager(
        [FromBody] GetFltrRGSupplyProjectManagerRequest request, CT ct)
    {
        var req = request.Adapt<GetFltrRGSupplyWithProductsRequest>();
        req.Statuses ??= new List<GoodsSupplyDetailStatus>
        {
            (GoodsSupplyDetailStatus)1,
            (GoodsSupplyDetailStatus)11,
            (GoodsSupplyDetailStatus)22
        };
        req.RemoveStatuses ??= new List<GoodsSupplyDetailStatus>
        {
            (GoodsSupplyDetailStatus)2
        };
        req.Types = new List<GoodsSupplyType>
        {
            (GoodsSupplyType)2
        };

        var result = await _logic.GetFltrRGSupplyWithProducts(req, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrProducts")]
    [ResponseSchema<GetFltrProductsResponse>]
    public async Task<IResult> GetFltrProducts(
        [FromBody] GetFltrProductsRequest request, CT ct)
    {
        var result = await _logic.GetFltrProducts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateRequestGoodsSupplyResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateRequestGoodsSupplyRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("Delete")]
    [ResponseSchema<DeleteRequestGoodsSupplyResponse>]
    public async Task<IResult> Delete(
        [FromQuery] DeleteRequestGoodsSupplyRequest request, CT ct)
    {
        var result = await _logic.DeleteRequestGoodsSupply(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetById")]
    [ResponseSchema<GetRequestGoodsSupplyByIdResponse>]
    public async Task<IResult> GetById(
        [FromQuery] GetRequestGoodsSupplyByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredRequestGoodsSuppliesReports")]
    [ResponseSchema<GetFilteredRequestGoodsSuppliesReportsResponse>]
    public async Task<IResult> GetFilteredRequestGoodsSuppliesReports(
        [FromBody] GetFilteredRequestGoodsSuppliesReportsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredRequestGoodsSuppliesReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetHistoryById")]
    [ResponseSchema<GetRequestGoodsHistoryByIdResponse>]
    public async Task<IResult> GetHistoryById(
        [FromBody] GetRequestGoodsHistoryByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetStatus")]
    [ResponseSchema<GetRequestGoodsSupplyStatusResponse>]
    public async Task<IResult> GetStatus(
        [FromBody] GetRequestGoodsSupplyStatusRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTypes")]
    [ResponseSchema<GetRequestGoodsSupplyTypeResponse>]
    public async Task<IResult> GetTypes(
        [FromQuery] GetRequestGoodsSupplyTypeRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSupplyTypes")]
    [Description("Get enum values for supply Type filtering.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetSupplyTypes(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetSupplyTypes");
        var result = EnumExtensions.GetEnums<GoodsSupplyType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpGet("GetsRequestGoodSupplyRequester")]
    [ResponseSchema<GetsRequestGoodSupplyRequesterResponse>]
    public async Task<IResult> GetsRequestGoodSupplyRequester(
        [FromQuery] GetsRequestGoodSupplyRequesterRequest request, CT ct)
    {
        var result = await _logic.GetsRequestGoodSupplyRequester(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestGoodsSupplyCreators")]
    [ResponseSchema<GetRequestGoodsSupplyCreatorsResponse>]
    public async Task<IResult> GetRequestGoodsSupplyCreators(
        [FromBody] GetRequestGoodsSupplyCreatorsRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyCreators(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestGoodsSupplyProductGroups")]
    [ResponseSchema<GetRequestGoodsSupplyProductGroupsResponse>]
    public async Task<IResult> GetRequestGoodsSupplyProductGroups(
        [FromQuery] GetRequestGoodsSupplyProductGroupsRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyProductGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestGoodsSupplyProducts")]
    [ResponseSchema<GetRequestGoodsSupplyProductsResponse>]
    public async Task<IResult> GetRequestGoodsSupplyProducts(
        [FromBody] GetRequestGoodsSupplyProductsRequest request, CT ct)
    {
        var result = await _logic.GetRequestGoodsSupplyProducts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateRGSType")]
    [ResponseSchema<CreateRGSTypeResponse>]
    public async Task<IResult> CreateProductRGS(
        [FromBody] CreateRGSTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateRGSType");
        var result = await _mediator.Send(new CreateRGSTypeCommand(request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteRGSType")]
    [ResponseSchema<DeleteRGSTypeResponse>]
    public async Task<IResult> DeleteRGSType(
        [FromBody] DeleteRGSTypeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteRGSType");
        var result = await _mediator.Send(new DeleteRGSTypeCommand(request.Id), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetReferenceItems")]
    [ResponseSchema<GetReferenceItemsResponse>]
    public async Task<IResult> GetReferenceItems(
        [FromBody] GetReferenceItemsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetReferenceItems");
        var result = await _mediator.Send(new GetReferenceItemsQuery(request.FaFilter,
            request.EnFilter,
            request.SupplyTypes,
            request.PageIndex,
            request.PageSize), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateRGSType")]
    [ResponseSchema<UpdateRGSTypeResponse>]
    public async Task<IResult> UpdateRGSType(
    [FromBody] UpdateRGSTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateRGSType");
        var result = await _logic.UpdateRGSType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteRGS")]
    [ResponseSchema<DeleteRGSResponse>]
    public async Task<IResult> DeleteRGS(
        [FromBody] DeleteRGSRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteRGS");
        var result = await _logic.DeleteRGS(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RGSStatusChanger")]
    [ResponseSchema<RGSStatusChangerResponse>]
    public async Task<IResult> RGSStatusChanger(
        [FromBody] RGSStatusChangerRequest request, CT ct)
    {
        var result = await _logic.RGSStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRGSById")]
    [ResponseSchema<GetRGSByIdResponse>]
    public async Task<IResult> GetRGSById(
        [FromQuery] GetRGSByIdRequest request, CT ct)
    {
        var result = await _logic.GetRGSById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDetailByRGSTypeId")]
    [ResponseSchema<GetDetailByRGSTypeIdResponse>]
    public async Task<IResult> GetDetailByRGSTypeId(
        [FromBody] GetDetailByRGSTypeIdRequest request, CT ct)
    {
        var result = await _logic.GetDetailByRGSTypeId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDetailByRGSTypeIdExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetDetailByRGSTypeIdExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<DetailRGSTypeDetailEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetDetailByRGSTypeIdExcelExporter")]
    [ResponseSchema<GetDetailByRGSTypeIdExcelResponse>]
    public async Task<IResult> GetDetailByRGSTypeIdExcelExporter(
        [FromBody] GetDetailByRGSTypeIdExcelRequest request, CT ct)
    {
        var response = await _logic.GetDetailByRGSTypeId(request.Adapt<GetDetailByRGSTypeIdRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetDetailByRGSTypeIdExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "RGSTypeDetails"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectWbs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetDetailByRGSTypeIdExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetReferenceTypeHistory")]
    [ResponseSchema<GetReferenceTypeHistoryResponse>]
    public async Task<IResult> GetReferenceTypeHistory(
        [FromBody] GetReferenceTypeHistoryRequest request, CT ct)
    {
        var result = await _logic.GetReferenceTypeHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetReferenceTypeHistoryExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetReferenceTypeHistory(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ReferenceTypeHistoryEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetReferenceTypeHistoryExcelExporter")]
    [ResponseSchema<GetReferenceTypeHistoryExcelResponse>]
    public async Task<IResult> GetReferenceTypeHistoryExporter(
        [FromBody] GetReferenceTypeHistoryExcelRequest request, CT ct)
    {
        var response = await _logic.GetReferenceTypeHistory(request.Adapt<GetReferenceTypeHistoryRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetReferenceTypeHistoryExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "TypeHistory"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ProjectWbs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetReferenceTypeHistoryExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetFltrRGS")]
    [ResponseSchema<GetFltrRGSResponse>]
    public async Task<IResult> GetFltrRGS(
        [FromBody] GetFltrRGSRequest request, CT ct)
    {
        var result = await _logic.GetFltrRGS(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrRGSExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetFltrRGSExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<FltrRGSEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetFltrRGSExcelExporter")]
    [ResponseSchema<GetFltrRGSExcelResponse>]
    public async Task<IResult> GetFltrRGSExcelExporter(
        [FromBody] GetFltrRGSExcelRequest request, CT ct)
    {
        var response = await _logic.GetFltrRGS(request.Adapt<GetFltrRGSRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetFltrRGSExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "RGS"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RGS-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetFltrRGSExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetDetailByRGSIdPdfReport")]
    public async Task<IResult> GetDetailByRGSIdPdfReport(
        [FromBody] GetDetailByRGSIdPdfReportRequest request, CT ct)
    {
        var result = await _detailPdf.Handle(request, ct);
        return result;
    }

    [HttpPost("GetDetailByRGSIdPdfEnReport")]
    public async Task<IResult> GetDetailByRGSIdPdfEnReport(
        [FromBody] GetDetailByRGSIdPdfEnReportRequest request, CT ct)
    {
        var result = await _detailEnPdf.Handle(request, ct);
        return result;
    }

    [HttpPost("GetDetailByRGSIdXslxReport")]
    [ResponseSchema<GetDetailByRGSIdXslxReportResponse>]
    public async Task<IResult> GetRGSProductXlsxReport(
        [FromBody] GetDetailByRGSIdXslxReportRequest request, CT ct)
    {
        var result = await _detailExcel.Handle(request, ct);
        return result;
    }

    [HttpPost("GetDetailByRGSIdXslxEnReport")]
    [ResponseSchema<GetDetailByRGSIdXslxReportResponse>]
    public async Task<IResult> GetDetailByRGSIdXslxEnReport(
        [FromBody] GetDetailByRGSIdXslxEnReportRequest request, CT ct)
    {
        var result = await _detailEnExcel.Handle(request, ct);
        return result;
    }

    [HttpPost("GetReferenceTypeHistoryFaReport")]
    public async Task<IResult> GetReferenceTypeHistoryFaReport(
        [FromBody] GetReferenceTypeHistoryRequest request, CT ct)
    {
        var result = await _referenceHistoryFaPdf.Handle(request, ct);
        return result;
    }

    [HttpGet("GetDetailByRGSId")]
    [ResponseSchema<GetDetailByRGSIdResponse>]
    public async Task<IResult> GetDetailByRGSId(
        [FromQuery] GetDetailByRGSIdRequest request, CT ct)
    {
        var result = await _logic.GetDetailByRGSId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRGSTypeByRGSId")]
    [ResponseSchema<GetRGSTypeByRGSIdResponse>]
    public async Task<IResult> GetRGSTypeByRGSId(
        [FromQuery] GetRGSTypeByRGSIdRequest request, CT ct)
    {
        var result = await _logic.GetRGSTypeByRGSId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRGSTypeByRGSIdExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetRGSTypeByRGSId(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<RGSTypeEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetRGSTypeByRGSIdExcelExporter")]
    [ResponseSchema<GetFltrRGSExcelResponse>]
    public async Task<IResult> GetRGSTypeByRGSIdExcelExporter(
        [FromBody] GetRGSTypeByRGSIdExcelRequest request, CT ct)
    {
        var response = await _logic.GetRGSTypeByRGSId(request.Adapt<GetRGSTypeByRGSIdRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetRGSTypeByRGSIdExcelResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, "RGS"),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RGS-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetRGSTypeByRGSIdExcelResponse?>(
            new(result)).GetHttpResponse();
    }

    [HttpPost("GetPurchaseLocation")]
    [Description("GetPurchaseLocation.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetPurchaseLocation(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<PurchaseLocation>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetPurchaseReason")]
    [Description("GetPurchaseReason.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetPurchaseReason(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<PurchaseReason>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }


    [HttpPost("GetRGSTypeStatus")]
    [Description("GetRGSTypeStatus.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetRGSTypeStatus(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetRGSTypeStatus");
        var result = EnumExtensions.GetEnums<RGSTypeStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetServiceReasonType")]
    [Description("GetServiceReasonType.")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetServiceReasonType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetServiceReasonType");
        var result = EnumExtensions.GetEnums<ServiceReasonType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }
}