using Engineering.Application.Services.Machineries.Models.ActiveMachinery;
using Engineering.Application.Services.Machineries.Models.CreateMachinery;
using Engineering.Application.Services.Machineries.Models.DisableMachinery;
using Engineering.Application.Services.Machineries.Models.GetActiveMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineryByCode;
using Engineering.Application.Services.Machineries.Models.GetMachineryById;
using Engineering.Application.Services.Machineries.Models.GetMachineryByName;
using Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;
using Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;
using Engineering.Application.Services.Machineries.Models.InactiveMachinery;
using Engineering.Application.Services.Machineries.Models.MachineryCodeCreator;
using Engineering.Application.Services.Machineries.Models.MachineryExcelImports;
using Engineering.Application.Services.Machineries.Models.MachineryGroupDelete;
using Engineering.Application.Services.Machineries.Models.StateChangerMachineries;
using Engineering.Application.Services.Machineries.Models.UpdateMachinery;

namespace Engineering.Application.Services.Machineries;

public interface IMachineryLogic
{
    ///Commands
    Task<Result<CreateMachineryResponse?>> CreateMachinery(
        CreateMachineryRequest request, CT ct);

    Task<Result<MachineryCodeCreatorResponse?>> MachineryCodeCreator(
        MachineryCodeCreatorRequest request, CT ct);

    Task<Result<MachineryExcelImportsResponse?>> MachineryExcelImports(
        MachineryExcelImportsRequest request, CT ct);

    Task<Result<DisableMachineryResponse?>> DisableMachinery(
        DisableMachineryRequest request, CT ct);

    Task<Result<UpdateMachineryResponse?>> UpdateMachinery(
        UpdateMachineryRequest request, CT ct);

    Task<Result<InactiveMachineryResponse?>> InactiveMachinery(
        InactiveMachineryRequest request, CT ct);

    Task<Result<ActiveMachineryResponse?>> ActiveMachinery(
        ActiveMachineryRequest request, CT ct);

    Task<Result<StateChangerMachineriesResponse?>> StateChangerMachineries(
        StateChangerMachineriesRequest request, CT ct);

    Task<Result<MachineryGroupDeleteResponse?>> MachineryGroupDelete(
        MachineryGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetMachineryByIdResponse?>> GetMachineryById(
        GetMachineryByIdRequest request, CT ct);

    Task<Result<GetMachineryByNameResponse?>> GetMachineryByName(
        GetMachineryByNameRequest request, CT ct);

    Task<Result<GetMachineryByCodeResponse?>> GetMachineryByCode(
        GetMachineryByCodeRequest request, CT ct);

    Task<Result<GetActiveMachineriesResponse?>> GetsActiveMachinery(
        GetActiveMachineriesRequest request, CT ct);

    Task<Result<GetMachineriesResponse?>> GetsMachinery(
        GetMachineriesRequest request, CT ct);

    Task<Result<GetsByMachineriesGroupIdResponse?>> GetsByMachineriesGroupId(
        GetsByMachineriesGroupIdRequest request, CT ct);

    Task<Result<GetsMachineryForRequestMachineryResponse?>> GetsMachineryForRequestMachinery(
        GetsMachineryForRequestMachineryRequest request, CT ct);

    Task<Result<GetsMachineryExcelEnumResponse?>> GetsMachineryExcelEnum(
        GetsMachineryExcelEnumRequest request, CT ct);

    Task<Result<GetsMachineryExcelExporterResponse?>> GetsMachineryExcelExporter(
        GetsMachineryExcelExporterRequest request, CT ct);

}