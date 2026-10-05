using Engineering.Application.Services.OperationInfos.Models.ActiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.AddSeasonsToOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.CodeCreator;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.DisableOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoContractors;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetsByMultiFilter;
using Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoByContractorIds;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;
using Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.InactiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoExcelImports;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoGroupDelete;
using Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
using Engineering.Application.Services.OperationInfos.Models.SetOperationInfoPriority;
using Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.UpdateOperationInfo;

namespace Engineering.Application.Services.OperationInfos;

public interface IOperationInfoLogic
{
    ///Commands
    Task<Result<CreateOperationInfoResponse?>> CreateOperationInfo(
        CreateOperationInfoRequest request, CT ct);

    Task<Result<CreateOperationInfoActionResponse>> CreateOperationInfoAction(
        CreateOperationInfoActionRequest request,
        CT ct);

    Task<Result<CreateOperationInfoActionsResponse>> CreateOperationInfoActions(
        CreateOperationInfoActionsRequest request,
        CT ct);

    Task<Result<DisableOperationInfoResponse?>> DisableOperationInfo(
        DisableOperationInfoRequest request, CT ct);

    Task<Result<UpdateOperationInfoResponse?>> UpdateOperationInfo(
        UpdateOperationInfoRequest request, CT ct);

    Task<Result<SetOperationInfoPriorityResponse?>> SetOperationInfoPriority(
        SetOperationInfoPriorityRequest request, CT ct);

    Task<Result<InactiveOperationInfoResponse?>> InactiveOperationInfo(
        InactiveOperationInfoRequest request, CT ct);

    Task<Result<ActiveOperationInfoResponse?>> ActiveOperationInfo(
        ActiveOperationInfoRequest request, CT ct);

    Task<Result<OperationInfoCodeCreatorResponse?>> CodeCreator(
        OperationInfoCodeCreatorRequest request, CT ct);

    Task<Result<StateChangerOperationInfosResponse?>> StateChangerOperationInfos(
        StateChangerOperationInfosRequest request, CT ct);

    Task<Result<OperationInfoGroupDeleteResponse?>> OperationInfoGroupDelete(
        OperationInfoGroupDeleteRequest request, CT ct);

    Task<Result<AddSeasonsToOperationInfosResponse?>> AddSeasonsToOperationInfos(
        AddSeasonsToOperationInfosRequest request, CT ct);

    ///Queries
    Task<Result<GetOperationInfoByIdResponse?>> GetOperationInfoById(
        GetOperationInfoByIdRequest request, CT ct);

    Task<Result<GetOperationInfoByNameResponse?>> GetOperationInfoByName(
        GetOperationInfoByNameRequest request, CT ct);

    Task<Result<GetOperationInfoByCodeResponse?>> GetOperationInfoByCode(
        GetOperationInfoByCodeRequest request, CT ct);

    Task<Result<GetOIActionByOperationInfoIdResponse>> GetOIActionByOperationInfoId(
        GetOIActionByOperationInfoIdRequest request, CT ct);

    Task<Result<GetActiveOperationInfosResponse?>> GetActiveOperationInfos(
        GetActiveOperationInfosRequest request, CT ct);

    Task<Result<GetOperationInfosResponse?>> GetOperationInfos(
        GetOperationInfosRequest request, CT ct);

    Task<Result<GetsPrioritizeOperationInfoResponse?>> GetsPrioritizeOperationInfo(
        GetsPrioritizeOperationInfoRequest request, CT ct);

    Task<Result<GetsByMultiFilterResponse?>> GetsByMultiFilter(
        GetsByMultiFilterRequest request, CT ct);

    Task<Result<GetsBySeasonIdResponse?>> GetsBySeasonId(
        GetsBySeasonIdRequest request, CT ct);

    Task<Result<GetOperationInfoContractorsResponse?>> GetOperationInfoContractors(
        GetOperationInfoContractorsRequest request, CT ct);

    Task<Result<GetsOperationInfoByContractorIdsResponse?>> GetsOperationInfoByContractorIds(
        GetsOperationInfoByContractorIdsRequest request, CT ct);

    Task<Result<GetsOperationInfoExcelExporterResponse?>> GetsOperationInfoExcelExporter(
        GetsOperationInfoExcelExporterRequest request, CT ct);

    Task<Result<GetsOperationInfoExcelEnumResponse?>> GetsOperationInfoExcelEnum(
        GetsOperationInfoExcelEnumRequest request, CT ct);

    Task<Result<GetsOperationInfoHistoryByIdResponse?>> GetsOperationInfoHistoryById(
        GetsOperationInfoHistoryByIdRequest request, CT ct);

    Task<Result<OperationInfoExcelImportsResponse>> OperationInfoExcelImports(
        OperationInfoExcelImportsRequest request, CT ct);

    Task<Result<GetActiveOperationInfoBySeasonIdsResponse?>> GetActiveOperationInfoBySeasonIds(
        GetActiveOperationInfoBySeasonIdsRequest request, CT ct);

    Task<Result<GetOperationInfoActionsResponse?>> GetOperationInfoActions(
        GetOperationInfoActionsRequest request, CT ct);

    Task<Result<DeleteOperationInfoActionResponse?>> DeleteOperationInfoAction(
        DeleteOperationInfoActionRequest request, CT ct);

    Task<Result<RasteReshteExcelImportsResponse>> RasteReshteExcelImports(
    RasteReshteExcelImportsRequest request, CT ct);

    Task<Result<FehrestBahaExcelImportsResponse>> FehrestBahaExcelImports(
    FehrestBahaExcelImportsRequest request,
    CT ct);

}