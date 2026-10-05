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

namespace Engineering.Application.Services.EmployerStatusStatements;

public interface IEmployerStatusStatementLogic
{
    Task<Result<CreateEmployerStatusStatementResponse?>> CreateEmployerStatusStatement(
        CreateEmployerStatusStatementRequest request, CT ct);

    Task<Result<ChangeESSStatusResponse?>> ChangeESSStatus(
        ChangeESSStatusRequest request, CT ct);

    Task<Result<EmployerStatusStatementCodeCreatorResponse?>> EmployerStatusStatementCodeCreator(
        EmployerStatusStatementCodeCreatorRequest request, CT ct);

    Task<Result<UpdateEmployerStatusStatementDocumentsResponse?>> UpdateEmployerStatusStatementDocuments(
        UpdateEmployerStatusStatementDocumentsRequest request, CT ct);

    Task<Result<UpdateESSProjectOperationDetailDailiesDocumentResponse?>> UpdateESSProjectOperationDetailDailiesDocument(
        UpdateESSProjectOperationDetailDailiesDocumentRequest request, CT ct);

    Task<Result<UpdateESSProjectOperationDetailDailyVolumeResponse?>> UpdateESSProjectOperationDetailDailyVolume(
        UpdateESSProjectOperationDetailDailyVolumeRequest request, CT ct);

    Task<Result<UpdateESSProjectOperationsDocumentResponse?>> UpdateESSProjectOperationsDocument(
        UpdateESSProjectOperationsDocumentRequest request, CT ct);

    Task<Result<UpdateESSProjectOperationsZeroVolumeResponse?>> UpdateESSProjectOperationsZeroVolume(
        UpdateESSProjectOperationsZeroVolumeRequest request, CT ct);

    Task<Result<GetEmployerStatusStatementByIdResponse?>> GetEmployerStatusStatementById(
        GetEmployerStatusStatementByIdRequest request, CT ct);

    Task<Result<GetsFilteredEmployerStatusStatementResponse?>> GetsFilteredEmployerStatusStatement(
        GetsFilteredEmployerStatusStatementRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementHistoryResponse?>> GetsEmployerStatusStatementHistory(
        GetsEmployerStatusStatementHistoryRequest request, CT ct);

    Task<Result<GetsFilteredEmployerRequesterResponse?>> GetsFilteredEmployerRequester(
        GetsFilteredEmployerRequesterRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementStatusResponse?>> GetsEmployerStatusStatementStatus(
        GetsEmployerStatusStatementStatusRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementTypeResponse?>> GetsEmployerStatusStatementType(
        GetsEmployerStatusStatementTypeRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementExcelEnumsResponse?>> GetsEmployerStatusStatementExcelEnums(
        GetsEmployerStatusStatementExcelEnumsRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementExcelExporterResponse?>> GetsEmployerStatusStatementExcelExporter(
        GetsEmployerStatusStatementExcelExporterRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementProjectOperationResponse?>> GetsEmployerStatusStatementProjectOperation(
        GetsEmployerStatusStatementProjectOperationRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementProjectOperationDetailResponse?>> GetsEmployerStatusStatementProjectOperationDetail(
        GetsEmployerStatusStatementProjectOperationDetailRequest request, CT ct);

    Task<Result<GetsEmployerStatusStatementProjectOperationDetailDailyResponse?>> GetsEmployerStatusStatementProjectOperationDetailDaily(
        GetsEmployerStatusStatementProjectOperationDetailDailyRequest request, CT ct);

    Task<Result<GetEmployerStatusStatementExcelExporterResponse?>> GetEmployerStatusStatementExcelExporter(
        GetEmployerStatusStatementExcelExporterRequest request, CT ct);

    Task<Result<GetEmployerStatusStatementLetterheadExcelResponse?>> GetEmployerStatusStatementLetterheadExcel(
        GetEmployerStatusStatementLetterheadExcelRequest request, CT ct);

}
