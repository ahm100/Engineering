using Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderGroupStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;
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
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContractDetailPrices;

namespace Engineering.Application.Services.ContractorContracts;

public interface IContractorContractLogic
{
    //Command
    Task<Result<CreateContractorContractResponse?>> CreateContractorContract(
        CreateContractorContractRequest request, CT ct);

    Task<Result<UpdateContractorContractResponse?>> UpdateContractorContract(
        UpdateContractorContractRequest request, CT ct);

    Task<Result<ContractorContractHeaderStatusChangerResponse?>> ContractorContractHeaderStatusChanger(
        ContractorContractHeaderStatusChangerModelRequest request, CT ct);

    Task<Result<ContractorContractHeaderGroupStatusChangerResponse?>> ContractorContractHeaderGroupStatusChanger(
        ContractorContractHeaderGroupStatusChangerRequest request, CT ct);

    Task<Result<DeleteContractorContractHeaderResponse?>> DeleteContractorContractHeader(
        DeleteContractorContractHeaderRequest request, CT ct);

    Task<Result<DeleteContractorContractResponse?>> DeleteContractorContract(
        DeleteContractorContractRequest request, CT ct);

    Task<Result<UpdateContractorContractDetailPricesResponse?>> UpdateContractorContractDetailPrices(
        UpdateContractorContractDetailPricesRequest request, CT ct);

    //Query
    Task<Result<GetDraftedFixCCsResponse>> GetDraftedFixCCs(
        GetDraftedFixCCsRequest request, CT ct);

    Task<Result<GetDraftedServiceCCsResponse>> GetDraftedServiceCCs(
        GetDraftedServiceCCsRequest request, CT ct);

    Task<Result<GetOperationFilteredSuggestedPriceHistoriesResponse?>> GetsOperationFilteredSuggestedPriceHistory(
        GetOperationFilteredSuggestedPriceHistoriesRequest request, CT ct);

    Task<Result<GetsContractorContractDetailPriceResponse?>> GetsContractorContractDetailPrice(
        GetsContractorContractDetailPriceRequest request, CT ct);

    Task<Result<GetContractorContractsDateResponse?>> GetContractorContractsDate(
        GetContractorContractsDateRequest request, CT ct);

    Task<Result<GetServiceFilteredSuggestedPriceHistoriesResponse?>> GetsServiceFilteredSuggestedPriceHistory(
        GetServiceFilteredSuggestedPriceHistoriesRequest request, CT ct);

    Task<Result<GetServicePriceHistoryResponse?>> GetServicePriceHistory(
        GetServicePriceHistoryRequest request, CT ct);

    Task<Result<GetsRequestedOperationContractResponse?>> GetsRequestedOperationContract(
        GetsRequestedOperationContractRequest request, CT ct);

    Task<Result<GetsRequestedServiceContractResponse?>> GetsRequestedServiceContract(
        GetsRequestedServiceContractRequest request, CT ct);

    Task<Result<GetsContractorByContractorContractTypeResponse?>> GetsContractorByContractorContractType(
        GetsContractorByContractorContractTypeRequest request, CT ct);

    Task<Result<GetFilteredContractorContractHeaderInfoResponse?>> GetsFilteredContractorContractHeaderInfo(
        GetFilteredContractorContractHeaderInfoRequest request, CT ct);

    Task<Result<GetsFilteredContractorContractHeaderResponse?>> GetsFilteredContractorContractHeader(
        GetsFilteredContractorContractHeaderRequest request, CT ct);

    Task<Result<GetsFilteredContractorContractReportsResponse?>> GetsFilteredContractorContractReports(
        GetsFilteredContractorContractReportsRequest request, CT ct);

    Task<Result<GetsFilteredContractorContractDetailReportsResponse?>> GetsFilteredContractorContractDetailReports(
        GetsFilteredContractorContractDetailReportsRequest request, CT ct);

    Task<Result<GetsIntegratedProjectOperationDetailServiceResponse?>> GetsIntegratedProjectOperationDetailService(
        GetsIntegratedProjectOperationDetailServiceRequest request, CT ct);

    Task<Result<GetsPartialProjectOperationDetailServiceResponse?>> GetsPartialProjectOperationDetailService(
        GetsPartialProjectOperationDetailServiceRequest request, CT ct);

    Task<Result<GetContractorContractHistoryResponse?>> GetsContractorContractHistory(
        GetContractorContractHistoryRequest request, CT ct);

    Task<Result<GetContractorContractHeaderByIdResponse?>> GetContractorContractHeaderById(
        GetContractorContractHeaderByIdRequest request, CT ct);

    Task<Result<GetCCHByIdResponse?>> GetCCHById(
        GetCCHByIdRequest request, CT ct);

    Task<Result<GetCCByHeaderIdResponse?>> GetCCByHeaderId(
        GetCCByHeaderIdRequest request, CT ct);

    Task<Result<GetFilteredContractorContractsByContractorResponse?>> GetsFilteredContractorContractByContractor(
        GetFilteredContractorContractsByContractorRequest request, CT ct);

    Task<Result<GetsContractorContractServiceReportResponse?>> GetsContractorContractServiceReport(
        GetsContractorContractServiceReportRequest request, CT ct);

    Task<Result<GetContractorPriceHistoryResponse?>> GetContractorPriceHistory(
        GetContractorPriceHistoryRequest request, CT ct);

    Task<Result<GetSuggestedServicePriceResponse?>> GetSuggestedServicePrice(
        GetSuggestedServicePriceRequest request, CT ct);

    Task<Result<GetCCHVersionByCCHIdResponse?>> GetCCHVersionByCCHId(
         GetCCHVersionByCCHIdRequest request, CT ct);

    Task<Result<GetDraftedFixCCsEnumResponse?>> GetDraftedFixCCsEnum(
        GetDraftedFixCCsEnumRequest request, CT ct);

    Task<Result<GetDraftedFixCCsExporterResponse?>> GetDraftedFixCCsExporter(
        GetDraftedFixCCsExporterRequest request, CT ct);

    Task<Result<GetDraftedServiceCCsEnumResponse?>> GetDraftedServiceCCsEnum(
        GetDraftedServiceCCsEnumRequest request, CT ct);

    Task<Result<GetDraftedServiceCCsExporterResponse>> GetDraftedServiceCCsExporter(
         GetDraftedServiceCCsExporterRequest request, CT ct);

    Task<Result<GetContractsByProjectIdResponse?>> GetContractsByProjectId(
        GetContractsByProjectIdRequest request, CT ct);

    Task<Result<GetCCThirdPartiesResponse?>> GetCCThirdParties(
        GetCCThirdPartiesRequest request, CT ct);

    Task<Result<GetConfirmedCCDailyServicesResponse?>> GetConfirmedCCDailyServices(
        GetConfirmedCCDailyServicesRequest request, CT ct);

    Task<Result<GetFltrProjectContractorsResponse?>> GetFltrProjectContractors(
        GetFltrProjectContractorsRequest request, CT ct);
}
