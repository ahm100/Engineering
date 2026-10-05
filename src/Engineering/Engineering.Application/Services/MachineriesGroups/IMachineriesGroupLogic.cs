using Engineering.Application.Services.MachineriesGroups.Models.ActiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.DisableMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.GetActiveMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineryGroupsForRequestMachinery;
using Engineering.Application.Services.MachineriesGroups.Models.InactiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupCodeCreator;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupGroupDelete;
using Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups;

public interface IMachineriesGroupLogic
{
    ///Commands
    Task<Result<CreateMachineriesGroupResponse?>> CreateMachineriesGroup(
        CreateMachineriesGroupRequest request, CT ct);

    Task<Result<MachineriesGroupExcelImportsResponse?>> MachineriesGroupExcelImports(
        MachineriesGroupExcelImportsRequest request, CT ct);

    Task<Result<UpdateMachineriesGroupResponse?>> UpdateMachineriesGroup(
        UpdateMachineriesGroupRequest request, CT ct);

    Task<Result<ActiveMachineriesGroupResponse?>> ActiveMachineriesGroup(
        ActiveMachineriesGroupRequest request, CT ct);

    Task<Result<InactiveMachineriesGroupResponse?>> InactiveMachineriesGroup(
        InactiveMachineriesGroupRequest request, CT ct);

    Task<Result<DisableMachineriesGroupResponse?>> DisableMachineriesGroup(
        DisableMachineriesGroupRequest request, CT ct);

    Task<Result<MachineriesGroupCodeCreatorResponse?>> MachineriesGroupCodeCreator(
        MachineriesGroupCodeCreatorRequest request, CT ct);

    Task<Result<StateChangerMachineriesGroupsResponse?>> StateChangerMachineriesGroups(
        StateChangerMachineriesGroupsRequest request, CT ct);

    Task<Result<MachineriesGroupGroupDeleteResponse?>> MachineriesGroupGroupDelete(
        MachineriesGroupGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetMachineriesGroupByIdResponse?>> GetMachineriesGroupById(
        GetMachineriesGroupByIdRequest request, CT ct);

    Task<Result<GetMachineriesGroupByNameResponse?>> GetMachineriesGroupByName(
        GetMachineriesGroupByNameRequest request, CT ct);

    Task<Result<GetMachineriesGroupByCodeResponse?>> GetMachineriesGroupByCode(
        GetMachineriesGroupByCodeRequest request, CT ct);

    Task<Result<GetActiveMachineriesGroupsResponse?>> GetActiveMachineriesGroups(
        GetActiveMachineriesGroupsRequest request, CT ct);

    Task<Result<GetMachineriesGroupsResponse?>> GetMachineriesGroups(
        GetMachineriesGroupsRequest request, CT ct);

    Task<Result<GetsMachineryGroupsForRequestMachineryResponse?>> GetsMachineryGroupsForRequestMachinery(
        GetsMachineryGroupsForRequestMachineryRequest request, CT ct);

    Task<Result<GetsMachineriesGroupExcelExporterResponse?>> GetsMachineriesGroupExcelExporter(
        GetsMachineriesGroupExcelExporterRequest request, CT ct);

    Task<Result<GetsMachineriesGroupExcelEnumResponse?>> GetsMachineriesGroupExcelEnum(
        GetsMachineriesGroupExcelEnumRequest request, CT ct);
}