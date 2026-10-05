using Engineering.Application.Services;
using Engineering.Application.Services.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSCreators;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Exporter;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Exporter;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementCodeCreator;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDiscountOperations;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Models.CreateContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.CSSGroupStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Models.DeleteContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementStatus;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelEnum;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelExporter;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;
using Engineering.Application.Services.ContractorStatusStatements.Models.UpdateContractorStatusStatement;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

[Authorize]
[Route("api/engineering/v1/ContractorStatusStatement")]
public class ContractorStatusStatementController : ControllerBase
{
    private readonly ILogger<ContractorStatusStatementController> _logger;
    private readonly IContractorStatusStatementLogic _logic;

    public ContractorStatusStatementController(
        ILogger<ContractorStatusStatementController> logger,
        IContractorStatusStatementLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateContractorStatusStatement")]
    [ResponseSchema<CreateContractorStatusStatementResponse>]
    public async Task<IResult> CreateContractorStatusStatement(
    [FromBody] CreateContractorStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractorStatusStatement");
        var result = await _logic.CreateContractorStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CSSGroupStatusChanger")]
    [ResponseSchema<CSSGroupStatusChangerResponse>]
    public async Task<IResult> CSSGroupStatusChanger(
        [FromBody] CSSGroupStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("CSSGroupStatusChanger");
        var result = await _logic.CSSGroupStatusChanger(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCStatementSContracts")]
    [ResponseSchema<GetCStatementSContractsResponse>]
    public async Task<IResult> GetCStatementSContracts(
        [FromQuery] GetCStatementSContractsRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementSContracts");
        var result = await _logic.GetCStatementSContracts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCStatementFContracts")]
    [ResponseSchema<GetCStatementFContractsResponse>]
    public async Task<IResult> GetCStatementFContracts(
        [FromQuery] GetCStatementFContractsRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementFContracts");
        var result = await _logic.GetCStatementFContracts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateContractorStatusStatement")]
    [ResponseSchema<UpdateContractorStatusStatementResponse>]
    public async Task<IResult> UpdateContractorStatusStatement(
        [FromBody] UpdateContractorStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractorStatusStatement");
        var result = await _logic.UpdateContractorStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ContractorStatusStatementDiscountOperations")]
    [ResponseSchema<ContractorStatusStatementDiscountOperationsResponse>]
    public async Task<IResult> ContractorStatusStatementDiscountOperations(
        [FromBody] ContractorStatusStatementDiscountOperationsRequest request, CT ct)
    {
        _logger.LogInformation("ContractorStatusStatementDiscountOperations");
        var result = await _logic.ContractorStatusStatementDiscountOperations(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ContractorStatusStatementCodeCreator")]
    [ResponseSchema<ContractorStatusStatementCodeCreatorResponse>]
    public async Task<IResult> ContractorStatusStatementCodeCreator(
        [FromBody] ContractorStatusStatementCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("ContractorStatusStatementCodeCreator");
        var result = await _logic.ContractorStatusStatementCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("ContractorStatusStatementDraftCreator")]
    [ResponseSchema<ContractorStatusStatementDraftCreatorResponse>]
    public async Task<IResult> ContractorStatusStatementDraftCreator(
        [FromQuery] ContractorStatusStatementDraftCreatorRequest request, CT ct)
    {
        _logger.LogInformation("ContractorStatusStatementDraftCreator");
        var result = await _logic.ContractorStatusStatementDraftCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorStatusStatementById")]
    [ResponseSchema<GetContractorStatusStatementByIdResponse>]
    public async Task<IResult> GetContractorStatusStatementById(
        [FromQuery] GetContractorStatusStatementByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorStatusStatementById");
        var result = await _logic.GetContractorStatusStatementById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorStatusStatementStatus")]
    [ResponseSchema<GetContractorStatusStatementStatusResponse>]
    public async Task<IResult> GetContractorStatusStatementStatus(
        [FromQuery] GetContractorStatusStatementStatusRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorStatusStatementStatus");
        var result = await _logic.GetContractorStatusStatementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredContractorStatusStatement")]
    [ResponseSchema<GetFilteredContractorStatusStatementResponse>]
    public async Task<IResult> GetFilteredContractorStatusStatement(
        [FromBody] GetFilteredContractorStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("GetFilteredContractorStatusStatement");
        var result = await _logic.GetFilteredContractorStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorStatusStatementExcelExporter")]
    [ResponseSchema<GetsContractorStatusStatementExcelExporterResponse>]
    public async Task<IResult> GetsContractorStatusStatementExcelExporter(
        [FromBody] GetsContractorStatusStatementExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorStatusStatementExcelExporter");
        var result = await _logic.GetsContractorStatusStatementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorStatusStatementHistory")]
    [ResponseSchema<GetsContractorStatusStatementHistoryResponse>]
    public async Task<IResult> GetsContractorStatusStatementHistory(
        [FromBody] GetsContractorStatusStatementHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorStatusStatementHistory");
        var result = await _logic.GetsContractorStatusStatementHistory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorStatusStatementExcelEnum")]
    [ResponseSchema<GetsContractorStatusStatementExcelEnumResponse>]
    public async Task<IResult> GetsContractorStatusStatementExcelEnum(
        [FromQuery] GetsContractorStatusStatementExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorStatusStatementExcelEnum");
        var result = await _logic.GetsContractorStatusStatementExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorStatusStatementDiscountById")]
    [ResponseSchema<GetsContractorStatusStatementDiscountByIdResponse>]
    public async Task<IResult> GetsContractorStatusStatementDiscountById(
        [FromQuery] GetsContractorStatusStatementDiscountByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorStatusStatementDiscountById");
        var result = await _logic.GetsContractorStatusStatementDiscountById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToProjectManagerConfirmed")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToProjectManagerConfirmed(
        [FromBody] SetContractorStatementToProjectManagerConfirmedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToProjectManagerConfirmed");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ProjectManagerConfirmed,
            null,
            null,
            null,
            request.ProjectManagerConfirmedAmount,
            request.DailyServices,
            request.FixedContractPcts,
            null,
            request.SelectedUrls,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToProjectManagerPending")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToProjectManagerPending(
        [FromBody] SetContractorStatementToProjectManagerPendingRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToProjectManagerPending");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ProjectManagerPending,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToReturnToProjectManager")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToReturnToProjectManager(
        [FromBody] SetContractorStatementToReturnToProjectManagerRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToReturnToProjectManager");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ReturnToProjectManager,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToProjectManagerRejected")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToProjectManagerRejected(
        [FromBody] SetContractorStatementToProjectManagerRejectedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToProjectManagerRejected");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ProjectManagerRejected,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToProjectManagerReturned")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToProjectManagerReturned(
    [FromBody] SetContractorStatementToProjectManagerReturnedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToProjectManagerReturned");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ProjectManagerReturned,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToManagementConfirmed")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToManagementConfirmed(
        [FromBody] SetContractorStatementToManagementConfirmedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToManagementConfirmed");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ManagementConfirmed,
            null,
            null,
            null,
            request.ManagementConfirmedAmount,
            request.DailyServices,
            request.FixedContractPcts,
            null,
            request.SelectedUrls,
            request.Description,
            request.MultiPayment,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToManagementPending")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToManagementPending(
        [FromBody] SetContractorStatementToManagementPendingRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToManagementPending");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ManagementPending,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToManagementRejected")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToManagementRejected(
        [FromBody] SetContractorStatementToManagementRejectedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToManagementRejected");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ManagementRejected,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToManagementReturnForReview")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToManagementReturnForReview(
        [FromBody] SetContractorStatementToManagementReturnForReviewRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToManagementReturnForReview");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ManagementReturnForReview,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToManagementReturned")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToManagementReturned(
        [FromBody] SetContractorStatementToManagementReturnedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToManagementReturned");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.ManagementReturned,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToPaymentConfirmation")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToPaymentConfirmation(
        [FromBody] SetContractorStatementToPaymentConfirmationRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToPaymentConfirmation");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.PaymentConfirmation,
            request.SeasonId,
            request.ConfirmedPaymentDate,
            request.ConfirmedBankAccountId,
            request.ConfirmedPrice,
            null,
            null,
            request.Urls,
            request.SelectedUrls,
            request.Description,
            null,
            request.CostCategoryId,
            request.CostGroupId,
            request.DocumentTypeId,
            request.PreferentialTypeId);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToPrimaryManagerRejected")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToPrimaryManagerRejected(
        [FromBody] SetContractorStatementToPrimaryManagerRejectedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToPrimaryManagerRejected");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.PrimaryManagerRejected,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToPrimaryManagerConfirmed")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToPrimaryManagerConfirmed(
        [FromBody] SetContractorStatementToPrimaryManagerConfirmedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToPrimaryManagerConfirmed");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.PrimaryManagerConfirmed,
            null,
            null,
            null,
            request.ConfirmedAmount,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToFinalManagerRejected")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToFinalManagerRejected(
        [FromBody] SetContractorStatementToFinalManagerRejectedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToFinalManagerRejected");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
            request.Id,
            CSSStatus.FinalManagerRejected,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            request.Description,
            null,
            null,
            null,
            null,
            null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorStatementToFinalManagerConfirmed")]
    [ResponseSchema<ContractorStatusStatementStatusChangerResponse>]
    public async Task<IResult> SetContractorStatementToFinalManagerConfirmed(
    [FromBody] SetContractorStatementToFinalManagerConfirmedRequest request, CT ct)
    {
        _logger.LogInformation("SetContractorStatementToFinalManagerConfirmed");
        var requestModel = new ContractorStatusStatementStatusChangerRequest(
           request.Id,
           CSSStatus.FinalManagerConfirmed,
           null,
           null,
           null,
           request.ConfirmedAmount,
           null,
           null,
           null,
           null,
           request.Description,
           null,
           null,
           null,
           null,
           null);
        var result = await _logic.ContractorStatusStatementStatusChanger(requestModel, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractorStatusStatement")]
    [ResponseSchema<DeleteContractorStatusStatementResponse>]
    public async Task<IResult> DeleteContractorStatusStatement(
        [FromQuery] DeleteContractorStatusStatementRequest request, CT ct)
    {
        _logger.LogInformation("DeleteContractorStatusStatement");
        var result = await _logic.DeleteContractorStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsIntegratedCSS")]
    [ResponseSchema<GetsIntegratedCSSResponse>]
    public async Task<IResult> GetsIntegratedCSS(
        [FromBody] GetsIntegratedCSSRequest request, CT ct)
    {
        _logger.LogInformation("GetsIntegratedCSS");
        var result = await _logic.GetsIntegratedCSS(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCSSDailyServiceUrls")]
    [ResponseSchema<GetCSSDailyServiceUrlsResponse>]
    public async Task<IResult> GetCSSDailyServiceUrls(
        [FromQuery] GetCSSDailyServiceUrlsRequest request, CT ct)
    {
        _logger.LogInformation("GetCSSDailyServiceUrls");
        var result = await _logic.GetCSSDailyServiceUrls(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCSSPayments")]
    [ResponseSchema<GetCSSPaymentsResponse>]
    public async Task<IResult> GetCSSPayments(
        [FromQuery] GetCSSPaymentsRequest request, CT ct)
    {
        _logger.LogInformation("GetCSSPayments");
        var result = await _logic.GetCSSPayments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCStatementFContractsEnum")]
    [ResponseSchema<GetCStatementFContractsEnumResponse>]
    public async Task<IResult> GetCStatementFContractsEnum(
        [FromQuery] GetCStatementFContractsEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementFContractsEnum");
        var result = await _logic.GetCStatementFContractsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetCStatementFContractsExporter")]
    [ResponseSchema<GetCStatementFContractsExporterResponse>]
    public async Task<IResult> GetCStatementFContractsExporter(
        [FromBody] GetCStatementFContractsExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementFContractsExporter");
        var result = await _logic.GetCStatementFContractsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCStatementSContractsEnum")]
    [ResponseSchema<GetCStatementSContractsEnumResponse>]
    public async Task<IResult> GetCStatementSContractsEnum(
        [FromQuery] GetCStatementSContractsEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementSContractsEnum");
        var result = await _logic.GetCStatementSContractsEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetCStatementSContractsExporter")]
    [ResponseSchema<GetCStatementSContractsExporterResponse>]
    public async Task<IResult> GetCStatementSContractsExporter(
        [FromBody] GetCStatementSContractsExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetCStatementSContractsExporter");
        var result = await _logic.GetCStatementSContractsExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetIntegratedCSSByProjectId")]
    [ResponseSchema<GetIntegratedCSSByProjectIdResponse>]
    public async Task<IResult> GetIntegratedCSSByProjectId(
        [FromBody] GetIntegratedCSSByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("GetIntegratedCSSByProjectId");
        var result = await _logic.GetIntegratedCSSByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCSSCreators")]
    [ResponseSchema<GetCSSCreatorsResponse>]
    public async Task<IResult> GetCSSCreators(
        [FromQuery] GetCSSCreatorsRequest request, CT ct)
    {
        _logger.LogInformation("GetCSSCreators");
        var result = await _logic.GetCSSCreators(request, ct);
        return result.GetHttpResponse();
    }
}