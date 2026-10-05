using Engineering.Application.Services.OperationInfoGroups.Models.ActiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.CodeCreator;
using Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.DisableOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetActiveOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;
using Engineering.Application.Services.OperationInfoGroups.Models.InactiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupExcelImports;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupGroupDelete;
using Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups;

public interface IOperationInfoGroupLogic
{
    ///Commands
    Task<Result<CreateOperationInfoGroupResponse?>> CreateOperationInfoGroup(
        CreateOperationInfoGroupRequest request, CT ct);

    Task<Result<OperationInfoGroupExcelImportsResponse?>> OperationInfoGroupExcelImports(
        OperationInfoGroupExcelImportsRequest request, CT ct);

    Task<Result<OperationInfoGroupCodeCreatorResponse?>> OperationInfoGroupCodeCreator(
        OperationInfoGroupCodeCreatorRequest request, CT ct);

    Task<Result<UpdateOperationInfoGroupResponse?>> UpdateOperationInfoGroup(
        UpdateOperationInfoGroupRequest request, CT ct);

    Task<Result<DisableOperationInfoGroupResponse?>> DisableOperationInfoGroup(
        DisableOperationInfoGroupRequest request, CT ct);

    Task<Result<InactiveOperationInfoGroupResponse?>> InactiveOperationInfoGroup(
        InactiveOperationInfoGroupRequest request, CT ct);

    Task<Result<ActiveOperationInfoGroupResponse?>> ActiveOperationInfoGroup(
        ActiveOperationInfoGroupRequest request, CT ct);

    Task<Result<StateChangerOperationInfoGroupsResponse?>> StateChangerOperationInfoGroups(
        StateChangerOperationInfoGroupsRequest request, CT ct);

    Task<Result<OperationInfoGroupGroupDeleteResponse?>> OperationInfoGroupGroupDelete(
        OperationInfoGroupGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetOperationInfoGroupByIdResponse?>> GetOperationInfoGroupById(
        GetOperationInfoGroupByIdRequest request, CT ct);

    Task<Result<GetsOperationInfoGroupResponse?>> GetsOperationInfoGroup(
        GetsOperationInfoGroupRequest request, CT ct);

    Task<Result<GetOperationInfoGroupByNameResponse?>> GetOperationInfoGroupByName(
        GetOperationInfoGroupByNameRequest request, CT ct);

    Task<Result<GetOperationInfoGroupByCodeResponse?>> GetOperationInfoGroupByCode(
        GetOperationInfoGroupByCodeRequest request, CT ct);

    Task<Result<GetActiveOperationInfoGroupsResponse?>> GetActiveOperationInfoGroups(
        GetActiveOperationInfoGroupsRequest request, CT ct);

    Task<Result<GetsOperationInfoGroupExcelExporterResponse?>> GetsOperationInfoGroupExcelExporter(
        GetsOperationInfoGroupExcelExporterRequest request, CT ct);

    Task<Result<GetsOperationInfoGroupExcelEnumResponse?>> GetsOperationInfoGroupExcelEnum(
        GetsOperationInfoGroupExcelEnumRequest request, CT ct);

}