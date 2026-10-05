using Engineering.Api.Extensions.Enums;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;
using Engineering.Application.Services.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderGroupStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorByContractorContractType;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContractDetailPrices;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using System.ComponentModel;

[Authorize]
[Route("api/engineering/v1/ContractorContract")]
public class ContractorContractController : ControllerBase
{
    private readonly ILogger<ContractorContractController> _logger;
    private readonly IContractorContractLogic _logic;

    public ContractorContractController(
        ILogger<ContractorContractController> logger,
        IContractorContractLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateContractorContract")]
    [ResponseSchema<CreateContractorContractResponse>]
    public async Task<IResult> CreateContractorContract(
        [FromBody] CreateContractorContractRequest request,
        CT ct)
    {
        var result = await _logic.CreateContractorContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetConfirmedCCDailyServices")]
    [ResponseSchema<GetConfirmedCCDailyServicesResponse>]
    public async Task<IResult> GetConfirmedCCDailyServices(
        [FromBody] GetConfirmedCCDailyServicesRequest request,
        CT ct)
    {
        var result = await _logic.GetConfirmedCCDailyServices(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDraftedFixCCs")]
    [ResponseSchema<GetDraftedFixCCsResponse>]
    public async Task<IResult> GetDraftedFixCCs(
        [FromQuery] GetDraftedFixCCsRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedFixCCs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDraftedServiceCCs")]
    [ResponseSchema<GetDraftedServiceCCsResponse>]
    public async Task<IResult> GetDraftedServiceCCs(
        [FromQuery] GetDraftedServiceCCsRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedServiceCCs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractorContract")]
    [ResponseSchema<UpdateContractorContractResponse>]
    public async Task<IResult> UpdateContractorContract(
        [FromBody] UpdateContractorContractRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractorContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ContractorContractHeaderGroupStatusChanger")]
    [ResponseSchema<ContractorContractHeaderGroupStatusChangerResponse>]
    public async Task<IResult> ContractorContractHeaderGroupStatusChanger(
        [FromBody] ContractorContractHeaderGroupStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.ContractorContractHeaderGroupStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToArchived")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToArchived(
        [FromBody] SetHeaderStatusToArchivedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.Archived, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToManagementReturnForReview")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToManagementReturnForReview(
        [FromBody] SetHeaderStatusToManagementReturnForReviewRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ManagementReturnForReview, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToManagementConfirmed")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToManagementConfirmed(
        [FromBody] SetHeaderStatusToManagementConfirmedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ManagementConfirmed, request.Description, request.Urls);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToManagementRejected")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToManagementRejected(
        [FromBody] SetHeaderStatusToManagementRejectedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ManagementRejected, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToManagementReturned")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToManagementReturned(
        [FromBody] SetHeaderStatusToManagementReturnedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ManagementReturned, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractorContractDetailPrices")]
    [ResponseSchema<UpdateContractorContractDetailPricesResponse>]
    public async Task<IResult> UpdateContractorContractDetailPrices(
        [FromBody] UpdateContractorContractDetailPricesRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractorContractDetailPrices(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToProjectManagerConfirmed")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToProjectManagerConfirmed(
        [FromBody] SetHeaderStatusToProjectManagerConfirmedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ProjectManagerConfirmed, request.Description, request.Urls);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToProjectManagerRejected")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToProjectManagerRejected(
        [FromBody] SetHeaderStatusToProjectManagerRejectedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ProjectManagerRejected, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetHeaderStatusToProjectManagerReturned")]
    [ResponseSchema<ContractorContractHeaderStatusChangerResponse>]
    public async Task<IResult> SetHeaderStatusToProjectManagerReturned(
        [FromBody] SetHeaderStatusToProjectManagerReturnedRequest request,
        CT ct)
    {
        var requestModel = new ContractorContractHeaderStatusChangerModelRequest(
            request.Id, null, ContractorContractStatus.ProjectManagerReturned, request.Description, null);
        var result = await _logic.ContractorContractHeaderStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorContractHeaderById")]
    [ResponseSchema<GetContractorContractHeaderByIdResponse>]
    public async Task<IResult> GetContractorContractHeaderById(
        [FromQuery] GetContractorContractHeaderByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractorContractHeaderById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorContractsDate")]
    [ResponseSchema<GetContractorContractsDateResponse>]
    public async Task<IResult> GetContractorContractsDate(
        [FromQuery] GetContractorContractsDateRequest request,
        CT ct)
    {
        var result = await _logic.GetContractorContractsDate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorPriceHistory")]
    [ResponseSchema<GetContractorPriceHistoryResponse>]
    public async Task<IResult> GetContractorPriceHistory(
        [FromQuery] GetContractorPriceHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetContractorPriceHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetStatus")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetStatus(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractStatus>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetCCType")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetCCType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpGet("GetsContractorByContractorContractType")]
    [ResponseSchema<GetsContractorByContractorContractTypeResponse>]
    public async Task<IResult> GetsContractorByContractorContractType(
        [FromQuery] GetsContractorByContractorContractTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorByContractorContractType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredByContractorId")]
    [ResponseSchema<GetFilteredContractorContractsByContractorResponse>]
    public async Task<IResult> GetFilteredByContractorId(
        [FromBody] GetFilteredContractorContractsByContractorRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredContractorContractByContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredContractorContractHeader")]
    [ResponseSchema<GetsFilteredContractorContractHeaderResponse>]
    public async Task<IResult> GetsFilteredContractorContractHeader(
        [FromBody] GetsFilteredContractorContractHeaderRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredContractorContractHeader(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSuggestedServicePrice")]
    [ResponseSchema<GetSuggestedServicePriceResponse>]
    public async Task<IResult> GetSuggestedServicePrice(
        [FromBody] GetSuggestedServicePriceRequest request,
        CT ct)
    {
        var result = await _logic.GetSuggestedServicePrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetServicePriceHistory")]
    [ResponseSchema<GetServicePriceHistoryResponse>]
    public async Task<IResult> GetServicePriceHistory(
        [FromBody] GetServicePriceHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetServicePriceHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetHistories")]
    [ResponseSchema<GetContractorContractHistoryResponse>]
    public async Task<IResult> GetHistories(
        [FromBody] GetContractorContractHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorContractHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSuggestedPriceOperation")]
    [ResponseSchema<GetOperationFilteredSuggestedPriceHistoriesResponse>]
    public async Task<IResult> GetSuggestedPriceOperation(
        [FromBody] GetOperationFilteredSuggestedPriceHistoriesRequest request,
        CT ct)
    {
        var result = await _logic.GetsOperationFilteredSuggestedPriceHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetSuggestedPriceService")]
    [ResponseSchema<GetServiceFilteredSuggestedPriceHistoriesResponse>]
    public async Task<IResult> GetSuggestedPriceService(
        [FromBody] GetServiceFilteredSuggestedPriceHistoriesRequest request,
        CT ct)
    {
        var result = await _logic.GetsServiceFilteredSuggestedPriceHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorContractDetailPrice")]
    [ResponseSchema<GetsContractorContractDetailPriceResponse>]
    public async Task<IResult> GetsContractorContractDetailPrice(
        [FromBody] GetsContractorContractDetailPriceRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorContractDetailPrice(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetHeaderInfo")]
    [ResponseSchema<GetFilteredContractorContractHeaderInfoResponse>]
    public async Task<IResult> GetHeaderInfo(
        [FromBody] GetFilteredContractorContractHeaderInfoRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredContractorContractHeaderInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestedOperationContract")]
    [ResponseSchema<GetsRequestedOperationContractResponse>]
    public async Task<IResult> GetsRequestedOperationContract(
        [FromBody] GetsRequestedOperationContractRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestedOperationContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestedServiceContract")]
    [ResponseSchema<GetsRequestedServiceContractResponse>]
    public async Task<IResult> GetsRequestedServiceContract(
        [FromBody] GetsRequestedServiceContractRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestedServiceContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsIntegratedProjectOperationDetailService")]
    [ResponseSchema<GetsIntegratedProjectOperationDetailServiceResponse>]
    public async Task<IResult> GetsIntegratedProjectOperationDetailService(
        [FromBody] GetsIntegratedProjectOperationDetailServiceRequest request,
        CT ct)
    {
        var result = await _logic.GetsIntegratedProjectOperationDetailService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsPartialProjectOperationDetailService")]
    [ResponseSchema<GetsPartialProjectOperationDetailServiceResponse>]
    public async Task<IResult> GetsPartialProjectOperationDetailService(
        [FromBody] GetsPartialProjectOperationDetailServiceRequest request,
        CT ct)
    {
        var result = await _logic.GetsPartialProjectOperationDetailService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredContractorContractReports")]
    [ResponseSchema<GetsFilteredContractorContractReportsResponse>]
    public async Task<IResult> GetsFilteredContractorContractReports(
        [FromBody] GetsFilteredContractorContractReportsRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredContractorContractReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredContractorContractDetailReports")]
    [ResponseSchema<GetsFilteredContractorContractDetailReportsResponse>]
    public async Task<IResult> GetsFilteredContractorContractDetailReports(
        [FromBody] GetsFilteredContractorContractDetailReportsRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredContractorContractDetailReports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorContractExcelExporter")]
    [Description("Export filtered to Excel.")]
    [ResponseSchema<GetsContractorContractExcelExporterResponse>]
    public async Task<IResult> GetsContractorContractExcelExporter(
        [FromBody] GetsContractorContractExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation($"GetsContractorContractExcelExporter");
        var response = await _logic.GetsFilteredContractorContractHeader(
            request.Adapt<GetsFilteredContractorContractHeaderRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorContractExcelExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetsContractorContractExcelExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractReportsExcelExporter")]
    [ResponseSchema<GetsContractorContractReportsExcelExporterResponse>]
    public async Task<IResult> GetsContractorContractReportsExcelExporter(
        [FromBody] GetsContractorContractReportsExcelExporterRequest request,
        CT ct)
    {
        _logger.LogInformation($"GetsContractorContractReportsExcelExporter");
        var response = await _logic.GetsFilteredContractorContractReports(
            request.Adapt<GetsFilteredContractorContractReportsRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorContractReportsExcelExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetsContractorContractReportsExcelExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractDetailReportsExcelExporter")]
    [ResponseSchema<GetsContractorContractDetailReportsExcelExporterResponse>]
    public async Task<IResult> GetsContractorContractDetailReportsExcelExporter(
        [FromBody] GetsContractorContractDetailReportsExcelExporterRequest request,
        CT ct)
    {
        _logger.LogInformation($"GetsContractorContractDetailReportsExcelExporter");
        var response = await _logic.GetsFilteredContractorContractDetailReports(
            request.Adapt<GetsFilteredContractorContractDetailReportsRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorContractDetailReportsExcelExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetsContractorContractDetailReportsExcelExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractServiceReport")]
    [ResponseSchema<GetsContractorContractServiceReportResponse>]
    public async Task<IResult> GetsContractorContractServiceReport(
        [FromBody] GetsContractorContractServiceReportRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorContractServiceReport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetConfirmedCCDailyServicesExporter")]
    [ResponseSchema<GetConfirmedCCDailyServicesExporterResponse>]
    public async Task<IResult> GetConfirmedCCDailyServicesExporter(
        [FromBody] GetConfirmedCCDailyServicesExporterRequest request,
        CT ct)
    {
        _logger.LogInformation($"GetConfirmedCCDailyServicesExporter");
        var response = await _logic.GetConfirmedCCDailyServices(
            request.Adapt<GetConfirmedCCDailyServicesRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetConfirmedCCDailyServicesExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetConfirmedCCDailyServicesExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractServiceReportExcelExporter")]
    [ResponseSchema<GetsContractorContractServiceReportExcelExporterResponse>]
    public async Task<IResult> GetsContractorContractServiceReportExcelExporter(
        [FromBody] GetsContractorContractServiceReportExcelExporterRequest request,
        CT ct)
    {
        _logger.LogInformation($"GetsContractorContractServiceReportExcelExporter");
        var response = await _logic.GetsContractorContractServiceReport(
            request.Adapt<GetsContractorContractServiceReportRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorContractServiceReportExcelExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetsContractorContractServiceReportExcelExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpDelete("DeleteContractorContract")]
    [ResponseSchema<DeleteContractorContractResponse>]
    public async Task<IResult> DeleteContractorContract(
        [FromQuery] DeleteContractorContractRequest request,
        CT ct)
    {
        var result = await _logic.DeleteContractorContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractorContractHeader")]
    [ResponseSchema<DeleteContractorContractHeaderResponse>]
    public async Task<IResult> DeleteContractorContractHeader(
        [FromQuery] DeleteContractorContractHeaderRequest request,
        CT ct)
    {
        var result = await _logic.DeleteContractorContractHeader(request, ct);
        return result.GetHttpResponse();
    }


    [HttpPost("GetSuggestedServicePriceExcelExporter")]
    [ResponseSchema<GetSuggestedServicePriceExcelExporterResponse>]
    public async Task<IResult> GetSuggestedServicePriceExcelExporter(
        [FromBody] GetSuggestedServicePriceExcelExporterRequest request,
        CT ct)
    {
        _logger.LogInformation($"GetSuggestedServicePriceExcelExporter");
        var response = await _logic.GetSuggestedServicePrice(
            request.Adapt<GetSuggestedServicePriceRequest>(), ct);
        if (response.IsFailure)
            return Result.Failure<GetSuggestedServicePriceExcelExporterResponse>(response.Error!).GetHttpResponse();

        var result = new FileContentResult(ExcelExporter.ExportToExcel(response.Value!.Data!, request.ExcelFilters, ""),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CCHeader-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<GetSuggestedServicePriceExcelExporterResponse?>(new(result)).GetHttpResponse();
    }

    [HttpGet("GetCCHVersionByCCHId")]
    [ResponseSchema<GetCCHVersionByCCHIdResponse>]
    public async Task<IResult> GetCCHVersionByCCHId(
        [FromQuery] GetCCHVersionByCCHIdRequest request,
        CT ct)
    {
        var result = await _logic.GetCCHVersionByCCHId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDraftedFixCCsEnum")]
    [ResponseSchema<GetDraftedFixCCsEnumResponse>]
    public async Task<IResult> GetDraftedFixCCsEnum(
        [FromQuery] GetDraftedFixCCsEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedFixCCsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDraftedFixCCsExporter")]
    [ResponseSchema<GetDraftedFixCCsExporterResponse>]
    public async Task<IResult> GetDraftedFixCCsExporter(
        [FromBody] GetDraftedFixCCsExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedFixCCsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDraftedServiceCCsEnum")]
    [ResponseSchema<GetDraftedServiceCCsEnumResponse>]
    public async Task<IResult> GetDraftedServiceCCsEnum(
        [FromQuery] GetDraftedServiceCCsEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedServiceCCsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDraftedServiceCCsExporter")]
    [ResponseSchema<GetDraftedServiceCCsExporterResponse>]
    public async Task<IResult> GetDraftedServiceCCsExporter(
        [FromBody] GetDraftedServiceCCsExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetDraftedServiceCCsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCCHById")]
    [ResponseSchema<GetCCHByIdResponse>]
    public async Task<IResult> GetCCHById(
        [FromQuery] GetCCHByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetCCHById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCCByHeaderId")]
    [ResponseSchema<GetCCByHeaderIdResponse>]
    public async Task<IResult> GetCCByHeaderId(
        [FromQuery] GetCCByHeaderIdRequest request,
        CT ct)
    {
        var result = await _logic.GetCCByHeaderId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrProjectContractors")]
    [ResponseSchema<GetFltrProjectContractorsResponse>]
    public async Task<IResult> GetFltrProjectContractors(
        [FromBody] GetFltrProjectContractorsRequest request,
        CT ct)
    {
        var result = await _logic.GetFltrProjectContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorContractDetailCooperationBasisType")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetsContractorContractDetailCooperationBasisType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractDetailCooperationBasis>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetConfirmedCCDailyServicesEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetConfirmedCCDailyServicesEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<GetConfirmedCCDailyServicesEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractDetailReportsExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetsContractorContractDetailReportsExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractDetailReportsExcelEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractReportsExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetsContractorContractReportsExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractReportsExcelEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetsContractorContractExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractReportsExcelEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetsContractorContractServiceReportExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetsContractorContractServiceReportExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<ContractorContractReportExcelEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("GetSuggestedServicePriceExcelEnum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetSuggestedServicePriceExcelEnum(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<SuggestedServicePriceEnum>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

}