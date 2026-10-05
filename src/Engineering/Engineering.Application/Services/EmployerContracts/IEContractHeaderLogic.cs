using Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;
using Engineering.Application.Services.EmployerContracts.Contracts.GetConsiderationTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEDocumentTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractVolume;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;

namespace Engineering.Application.Services.EmployerContracts;

public interface IEContractHeaderLogic
{
    Task<Result<CreateEContractHeaderResponse?>> CreateEContractHeader(
        CreateEContractHeaderRequest request, CT ct);

    Task<Result<CreateEContractResponse?>> CreateEContract(
        CreateEContractRequest request, CT ct);

    Task<Result<UpdateEContractHeaderResponse?>> UpdateEContractHeader(
        UpdateEContractHeaderRequest request, CT ct);

    Task<Result<UpdateEContractResponse?>> UpdateEContract(
        UpdateEContractRequest request, CT ct);

    Task<Result<GetEContractHeaderByIdResponse?>> GetEContractHeaderById(
        GetEContractHeaderByIdRequest request, CT ct);

    Task<Result<GetEContractByIdResponse?>> GetEContractById(
        GetEContractByIdRequest request, CT ct);

    Task<Result<GetFltrEContractHeadsResponse?>> GetFltrEContractHeads(
        GetFltrEContractHeadsRequest request, CT ct);

    Task<Result<GetFltrEContractsResponse?>> GetFltrEContracts(
        GetFltrEContractsRequest request, CT ct);

    Task<Result<GetEContractTypesResponse?>> GetEContractTypes(
        GetEContractTypesRequest request, CT ct);

    Task<Result<GetEContractStatusResponse?>> GetEContractStatus(
        GetEContractStatusRequest request, CT ct);

    Task<Result<GetEDocumentTypesResponse?>> GetEDocumentTypes(
        GetEDocumentTypesRequest request, CT ct);

    Task<Result<GetConsiderationTypesResponse?>> GetConsiderationTypes(
        GetConsiderationTypesRequest request, CT ct);

    Task<Result<DeleteEContractHeaderResponse?>> DeleteEContractHeader(
        DeleteEContractHeaderRequest request, CT ct);

    Task<Result<DeleteEContractResponse?>> DeleteEContract(
        DeleteEContractRequest request, CT ct);

    Task<Result<SetECStatusResponse?>> EContractStatusChanger(
        SetECStatusRequest request, CT ct);

    Task<Result<GetFltrEContractHeadsEnumResponse?>> GetFltrEContractHeadsEnum(
        GetFltrEContractHeadsEnumRequest request, CT ct);

    Task<Result<GetFltrEContractHeadsExporterResponse?>> GetFltrEContractHeadsExporter(
        GetFltrEContractHeadsExporterRequest request, CT ct);

    Task<Result<GetFltrEContractsEnumResponse?>> GetFltrEContractsEnum(
        GetFltrEContractsEnumRequest request, CT ct);

    Task<Result<GetFltrEContractsExporterResponse?>> GetFltrEContractsExporter(
        GetFltrEContractsExporterRequest request, CT ct);

    Task<Result<GetFltrEmployersResponse?>> GetFltrEmployers(
        GetFltrEmployersRequest request, CT ct);

    Task<Result<GetEContractHistoryResponse>> GetEContractHistory(
        GetEContractHistoryRequest request, CT ct);

    Task<Result<GetEContractOperationHistoryResponse>> GetEContractOperationHistory(
        GetEContractOperationHistoryRequest request, CT ct);

    Task<Result<GetEOProductHistoryResponse>> GetEOProductHistory(
        GetEOProductHistoryRequest request, CT ct);

    Task<Result<GetEOServiceHistoryResponse>> GetEOServiceHistory(
        GetEOServiceHistoryRequest request, CT ct);

    Task<Result<GetECThirdPartiesResponse?>> GetECThirdParties(
        GetECThirdPartiesRequest request, CT ct);

    Task<Result<UpdateEContractVolumeResponse?>> UpdateEContractVolumeService(
        UpdateEContractVolumeRequest request, CT ct);

    Task<Result<EContractFinancialCoverResponse?>> EContractFinancialCover(
        EContractFinancialCoverRequest request, CT ct);

    Task<Result<UpdateEContractFinancialResponse?>> UpdateEContractFinancial(
        UpdateEContractFinancialRequest request, CT ct);
}