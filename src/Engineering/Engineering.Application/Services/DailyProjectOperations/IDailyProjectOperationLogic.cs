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

namespace Engineering.Application.Services.DailyProjectOperations;

public interface IDailyProjectOperationLogic
{
    Task<Result<CreateDailyProjectOperationResponse?>> CreateDailyProjectOperation(
        CreateDailyProjectOperationRequest request, CT ct);

    Task<Result<UpdateDailyProjectOperationResponse?>> UpdateDailyProjectOperation(
        UpdateDailyProjectOperationRequest request, CT ct);

    Task<Result<UpdateDailyProjectOperationDocumentsResponse?>> UpdateDailyProjectOperationDocuments(
        UpdateDailyProjectOperationDocumentsRequest request, CT ct);

    Task<Result<DailyProjectOperationTelegramMessageSenderResponse?>> DailyProjectOperationTelegramMessageSender(
        DailyProjectOperationTelegramMessageSenderRequest request, CT ct);

    Task<Result<DeleteDailyProjectOperationResponse?>> DeleteDailyProjectOperation(
        DeleteDailyProjectOperationRequest request, CT ct);

    Task<Result<GetFilteredProjectOperationsByProjectIdsResponse?>> GetFilteredProjectOperationsByProjectIds(
        GetFilteredProjectOperationsByProjectIdsRequest request, CT ct);

    Task<Result<GetFilteredDailyProjectOperationsResponse?>> GetFilteredDailyProjectOperations(
        GetFilteredDailyProjectOperationsRequest request, CT ct);

    Task<Result<GetFilteredDailyProjectOperationDetailsResponse?>> GetFilteredDailyProjectOperationDetails(
        GetFilteredDailyProjectOperationDetailsRequest request, CT ct);

    Task<Result<GetDailyProjectOperationHistoriesResponse?>> GetDailyProjectOperationHistories(
        GetDailyProjectOperationHistoriesRequest request, CT ct);

    Task<Result<GetDailyProjectOperationByLegacyIdResponse?>> GetDailyProjectOperationByLegacyId(
        GetDailyProjectOperationByLegacyIdRequest request, CT ct);

    Task<Result<GetDailyProjectOperationByIdResponse?>> GetDailyProjectOperationById(
        GetDailyProjectOperationByIdRequest request, CT ct);

    Task<Result<GetFilteredMachineriesResponse?>> GetFilteredMachineries(
        GetFilteredMachineriesRequest request, CT ct);

    Task<Result<GetFilteredConsumableExpertsResponse?>> GetFilteredConsumableExperts(
        GetFilteredConsumableExpertsRequest request, CT ct);

    Task<Result<GetFilteredConsumableProductsResponse?>> GetFilteredConsumableProducts(
        GetFilteredConsumableProductsRequest request, CT ct);

    Task<Result<GetsDailyProjectOperationStatusResponse?>> GetsDailyProjectOperationStatus(
        GetsDailyProjectOperationStatusRequest request, CT ct);

    Task<Result<GetEmployerStatusStatementLimitDateResponse?>> GetEmployerStatusStatementLimitDate(
        GetEmployerStatusStatementLimitDateRequest request, CT ct);

    Task<Result<GetDailyProjectOperationContractorsResponse?>> GetDailyProjectOperationContractors(
        GetDailyProjectOperationContractorsRequest request, CT ct);

    Task<Result<GetDailyHistoriesExcelExporterResponse?>> GetDailyHistoriesExcelExporter(
        GetDailyHistoriesExcelExporterRequest request, CT ct);

    Task<Result<GetDailyHistoriesExcelEnumsResponse?>> GetDailyHistoriesExcelEnums(
        GetDailyHistoriesExcelEnumsRequest request, CT ct);

    Task<Result<GetsDetailedDailyProjectOperationExcelExporterResponse?>> GetsDetailedDailyProjectOperationExcelExporter(
        GetsDetailedDailyProjectOperationExcelExporterRequest request, CT ct);

    Task<Result<GetsDetailedDailyProjectOperationExcelEnumsResponse?>> GetsDetailedDailyProjectOperationExcelEnums(
        GetsDetailedDailyProjectOperationExcelEnumsRequest request, CT ct);

    Task<Result<GetsDetailedDailyProjectOperationResponse?>> GetsDetailedDailyProjectOperation(
        GetsDetailedDailyProjectOperationRequest request, CT ct);

    Task<Result<GetDetailedDailyOperationTotalsResponse?>> GetDetailedDailyOperationTotals(
        GetDetailedDailyOperationTotalsRequest request, CT ct);

    Task<Result<GetsDailyProjectOperationDocumentResponse?>> GetsDailyProjectOperationDocument(
        GetsDailyProjectOperationDocumentRequest request, CT ct);

    Task<Result<GetTotalsByProjectOperationDetailIdResponse?>> GetTotalsByProjectOperationDetailId(
        GetTotalsByProjectOperationDetailIdRequest request, CT ct);

    Task<Result<GetOperationTotalsResponse?>> GetOperationTotals(
        GetOperationTotalsRequest request, CT ct);

    Task<Result<GetsDailyProjectOperationServiceResponse?>> GetsDailyProjectOperationService(
        GetsDailyProjectOperationServiceRequest request, CT ct);

    Task<Result<GetsTotalDailyProjectOperationServiceResponse?>> GetsTotalDailyProjectOperationService(
        GetsTotalDailyProjectOperationServiceRequest request, CT ct);

    Task<Result<GetsDailyProjectOperationServiceExcelExporterResponse?>> GetsDailyProjectOperationServiceExcelExporter(
        GetsDailyProjectOperationServiceExcelExporterRequest request, CT ct);

    Task<Result<GetsDailyProjectOperationServiceExcelEnumsResponse?>> GetsDailyProjectOperationServiceExcelEnums(
        GetsDailyProjectOperationServiceExcelEnumsRequest request, CT ct);

    Task<Result<GetsDailyServiceInfoResponse?>> GetsDailyServiceInfo(
        GetsDailyServiceInfoRequest request, CT ct);

    Task<Result<GetsDailyCreatorResponse?>> GetsDailyCreator(
        GetsDailyCreatorRequest request, CT ct);

    Task<Result<GetDailyProjectOperationHistoryByIdResponse?>> GetDailyProjectOperationHistoryById(
        GetDailyProjectOperationHistoryByIdRequest request, CT ct);
}
