using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;
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

namespace Engineering.Application.Services.ContractorStatusStatements;

public interface IContractorStatusStatementLogic
{
    Task<Result<ContractorStatusStatementDraftCreatorResponse?>> ContractorStatusStatementDraftCreator(
        ContractorStatusStatementDraftCreatorRequest request, CT ct);

    Task<Result<GetsDraftableContractorContractHeaderResponse?>> GetsDraftableContractorContractHeader(
        GetsDraftableContractorContractHeaderRequest request, CT ct);

    Task<Result<GetCStatementSContractsResponse>> GetCStatementSContracts(
        GetCStatementSContractsRequest request, CT ct);

    Task<Result<GetCStatementFContractsResponse>> GetCStatementFContracts(
        GetCStatementFContractsRequest request, CT ct);

    Task<Result<CreateContractorStatusStatementResponse?>> CreateContractorStatusStatement(
        CreateContractorStatusStatementRequest request, CT ct);

    Task<Result<UpdateContractorStatusStatementResponse?>> UpdateContractorStatusStatement(
        UpdateContractorStatusStatementRequest request, CT ct);

    Task<Result<ContractorStatusStatementCodeCreatorResponse?>> ContractorStatusStatementCodeCreator(
        ContractorStatusStatementCodeCreatorRequest request, CT ct);

    Task<Result<ContractorStatusStatementDiscountOperationsResponse?>> ContractorStatusStatementDiscountOperations(
        ContractorStatusStatementDiscountOperationsRequest request, CT ct);

    Task<Result<DeleteContractorStatusStatementResponse?>> DeleteContractorStatusStatement(
        DeleteContractorStatusStatementRequest request, CT ct);

    Task<Result<ContractorStatusStatementStatusChangerResponse?>> ContractorStatusStatementStatusChanger(
        ContractorStatusStatementStatusChangerRequest request, CT ct);

    Task<Result<CSSGroupStatusChangerResponse?>> CSSGroupStatusChanger(
        CSSGroupStatusChangerRequest request, CT ct);

    Task<Result<GetContractorStatusStatementByIdResponse?>> GetContractorStatusStatementById(
        GetContractorStatusStatementByIdRequest request, CT ct);

    Task<Result<GetContractorStatusStatementStatusResponse?>> GetContractorStatusStatementStatus(
        GetContractorStatusStatementStatusRequest request, CT ct);

    Task<Result<GetFilteredContractorStatusStatementResponse?>> GetFilteredContractorStatusStatement(
        GetFilteredContractorStatusStatementRequest request, CT ct);

    Task<Result<GetsContractorStatusStatementExcelEnumResponse?>> GetsContractorStatusStatementExcelEnum(
        GetsContractorStatusStatementExcelEnumRequest request, CT ct);

    Task<Result<GetsContractorStatusStatementExcelExporterResponse?>> GetsContractorStatusStatementExcelExporter(
        GetsContractorStatusStatementExcelExporterRequest request, CT ct);

    Task<Result<GetsContractorStatusStatementDiscountByIdResponse?>> GetsContractorStatusStatementDiscountById(
        GetsContractorStatusStatementDiscountByIdRequest request, CT ct);

    Task<Result<GetsContractorStatusStatementHistoryResponse?>> GetsContractorStatusStatementHistory(
        GetsContractorStatusStatementHistoryRequest request, CT ct);

    Task<Result<GetsIntegratedCSSResponse?>> GetsIntegratedCSS(
        GetsIntegratedCSSRequest request, CT ct);

    Task<Result<GetCSSDailyServiceUrlsResponse>> GetCSSDailyServiceUrls(
        GetCSSDailyServiceUrlsRequest request, CT ct);

    Task<Result<GetCSSPaymentsResponse>> GetCSSPayments(
        GetCSSPaymentsRequest request, CT ct);

    Task<Result<GetCStatementFContractsEnumResponse>> GetCStatementFContractsEnum(
        GetCStatementFContractsEnumRequest request, CT ct);

    Task<Result<GetCStatementFContractsExporterResponse>> GetCStatementFContractsExporter(
         GetCStatementFContractsExporterRequest request, CT ct);

    Task<Result<GetCStatementSContractsEnumResponse>> GetCStatementSContractsEnum(
        GetCStatementSContractsEnumRequest request, CT ct);

    Task<Result<GetCStatementFContractsExporterResponse>> GetCStatementSContractsExporter(
         GetCStatementSContractsExporterRequest request, CT ct);

    Task<Result<GetIntegratedCSSByProjectIdResponse>> GetIntegratedCSSByProjectId(
         GetIntegratedCSSByProjectIdRequest request, CT ct);

    Task<Result<GetCSSCreatorsResponse>> GetCSSCreators(
        GetCSSCreatorsRequest request, CT ct);
}
